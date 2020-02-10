using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.SqlServer.Management.Smo;
using View = Microsoft.SqlServer.Management.Smo.View;
using Table = Microsoft.SqlServer.Management.Smo.Table;
using Trigger = Microsoft.SqlServer.Management.Smo.Trigger;
using System.Collections.Specialized;

namespace sp_update
{
    public partial class Form1 : Form
    {
        public string ConnectionString { get; private set; }


        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private ServerConnection serverKurulum()
        {
            ServerConnection serverConnection = new ServerConnection()
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["sp_update.Properties.Settings.Setting"].ConnectionString

            };
            return serverConnection;
        }

        #region TumSPleriGetir Database-->Local
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("sp isimleri kayıt ediliyor ", "Bilgilendirme Penceresi");
            lblUyari.Text = "Bu islem biraz zaman alabilir. Lütfen Bekleyiniz.";

            string spNameFilePath = ConfigurationManager.AppSettings["spNameFile"];
            string filePath = @spNameFilePath;

            Server server = new Server(serverKurulum());
            string dbName = ConfigurationManager.AppSettings["db"];
            Database db = server.Databases[dbName];
            List<SqlSmoObject> list = new List<SqlSmoObject>();
            DataTable dataTable = db.EnumObjects(DatabaseObjectTypes.StoredProcedure);
            foreach (DataRow row in dataTable.Rows)
            {
                string sSchema = (string)row["Schema"];
                if (sSchema == "sys" || sSchema == "INFORMATION_SCHEMA")
                    continue;

                StoredProcedure sp = (StoredProcedure)server.GetSmoObject(
                new Urn((string)row["Urn"]));
                if (!sp.IsSystemObject)
                    list.Add(sp);
                string name = sp.Schema + '.' + sp.Name;

                try
                {
                    TextWriter tw = new StreamWriter(spNameFilePath, true);
                    tw.Write(name);
                    tw.Write("\n");
                    tw.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("hata var" + ex);

                }
            }
            MessageBox.Show("sp isimleri kayıt edildi ", "Bilgilendirme Penceresi");
            lblUyari.Text = "";
        }

        #endregion

        #region SP
        private void btnSP_Click(object sender, EventArgs e)
        {

            string spFilePath = ConfigurationManager.AppSettings["spFile"];
            string spNameFilePath = ConfigurationManager.AppSettings["spNameFile"];
            string filePath = @spNameFilePath;
            List<string> spName = File.ReadAllLines(filePath).ToList();

            Server server = new Server(serverKurulum());
            string dbName = ConfigurationManager.AppSettings["db"];
            Database db = server.Databases[dbName];
            List<SqlSmoObject> list = new List<SqlSmoObject>();
            DataTable dataTable = db.EnumObjects(DatabaseObjectTypes.StoredProcedure);
            foreach (DataRow row in dataTable.Rows)
            {
                int count = 0;
                string sSchema = (string)row["Schema"];
                if (sSchema == "sys" || sSchema == "INFORMATION_SCHEMA")
                    continue;

                StoredProcedure sp = (StoredProcedure)server.GetSmoObject(
                new Urn((string)row["Urn"]));
                if (!sp.IsSystemObject)
                {
                    string name = sp.Schema + '.' + sp.Name;

                    foreach (var item in spName)
                    {
                        if (item.Equals(name))
                        {
                            list.Add(sp);
                            count++;
                        }
                    }
                }

                for (int i = 0; i < count; i++)
                {
                    try
                    {
                        TextWriter tw = new StreamWriter(spFilePath + sp.Schema + '.' + sp.Name + ".txt", true);
                        tw.Write(sp.TextHeader);
                        tw.Write("\n");
                        tw.Write(sp.TextBody);
                        tw.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("hata var: " + ex);
                    }
                }
            }
            MessageBox.Show("spler kayıt edildi.", "Bilgilendirme Penceresi");

        }

        #endregion

        #region     Trigger
        private void btnTrigger_Click(object sender, EventArgs e)
                {


                    string spFilePath = ConfigurationManager.AppSettings["spFile"];
                    string spNameFilePath = ConfigurationManager.AppSettings["spNameFile"];
                    string filePath = @spNameFilePath;
                    List<string> spName = File.ReadAllLines(filePath).ToList();

                    Server server = new Server(serverKurulum());
                    string dbName = ConfigurationManager.AppSettings["db"];
                    Database db = server.Databases[dbName];
                    List<SqlSmoObject> list = new List<SqlSmoObject>();
                    DataTable dataTable = db.EnumObjects(DatabaseObjectTypes.Table);


                    #region trigger
                    var allTables = dataTable.AsEnumerable()
                                .Select(row => (Table)server.GetSmoObject(new Urn(row["Urn"].ToString())))
                                .ToList();
                    var triggerTables = allTables.Where(t => spName.Any(x => t.Triggers.Contains(x))).ToList();
                    var scriptOptions = new ScriptingOptions();
                    scriptOptions.NoCollation = true;

                    foreach (var triggerTable in triggerTables)
                    {
                        var triggerList = triggerTable.Triggers.Cast<Trigger>().Where(t => spName.Contains(t.Name)).ToList();
                        foreach (var trigger in triggerList)
                        {
                            using (TextWriter tw = new StreamWriter(spFilePath + triggerTable.Schema + '.' + trigger.Name + ".txt", true))
                            {
                                var script = trigger.Script(scriptOptions).Cast<string>().Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
                                script.ForEach(y =>
                                {
                                    tw.Write(y);
                                    tw.Write("\nGO\n");
                                });
                                tw.Close();
                            }
                        }
                    }

                    MessageBox.Show("spler kayıt edildi.", "Bilgilendirme Penceresi");
                    #endregion
                }
        #endregion 

        #region Table
        private void btnTable_Click(object sender, EventArgs e)
        {
            lblUyari.Text = "Bu islem biraz zaman alabilir. Lütfen Bekleyiniz.";

            string spFilePath = ConfigurationManager.AppSettings["spFile"];
            string spNameFilePath = ConfigurationManager.AppSettings["spNameFile"];
            string filePath = @spNameFilePath;
            List<string> spName = File.ReadAllLines(filePath).ToList();

            Server server = new Server(serverKurulum());
            string dbName = ConfigurationManager.AppSettings["db"];
            Database db = server.Databases[dbName];
            List<SqlSmoObject> list = new List<SqlSmoObject>();
            DataTable dataTable = db.EnumObjects(DatabaseObjectTypes.Table);
            
            #region newTable
            var list2 = dataTable.AsEnumerable()
                        .Select(row => new
                        {
                            Schema = row["Schema"].ToString(),
                            Name = row["Name"].ToString(),
                            Urn = row["Urn"].ToString()
                        })
                        .Where(x => spName.Contains($"{x.Schema}.{x.Name}"))
                        .ToList();
            foreach (var row in list2)
            {
                string sSchema = row.Schema;
                if (sSchema == "sys" || sSchema == "INFORMATION_SCHEMA")
                    continue;
                var scriptOptions = new ScriptingOptions();
                scriptOptions.ExtendedProperties = true;
                //scriptOptions.DriPrimaryKey = true;
                scriptOptions.NoCollation = true;
                scriptOptions.DriDefaults = true;

                scriptOptions.Triggers = true;

                Table sp = (Table)server.GetSmoObject(
                new Urn(row.Urn));

                var tableList = new List<Table>();
                if (!sp.IsSystemObject)
                {
                    tableList.Add(sp);
                }


                var ss = sp.Triggers;
                try
                {
                    foreach (var item in tableList)
                    {
                        using (TextWriter tw = new StreamWriter(spFilePath + item.Schema + '.' + item.Name + ".txt", true))
                        {
                            var script = item.Script(scriptOptions).Cast<string>().Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
                            script.ForEach(y =>
                            {
                                tw.Write(y);
                                tw.Write("\nGO\n");
                            });
                            tw.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("hata var: " + ex);
                }
            }
            MessageBox.Show("spler kayıt edildi.", "Bilgilendirme Penceresi");
            #endregion
        }
        #endregion

        #region Functions
        private void btnFunctions_Click(object sender, EventArgs e)
        {
            string spFilePath = ConfigurationManager.AppSettings["spFile"];
            string spNameFilePath = ConfigurationManager.AppSettings["spNameFile"];
            string filePath = @spNameFilePath;
            List<string> spName = File.ReadAllLines(filePath).ToList();

            Server server = new Server(serverKurulum());
            string dbName = ConfigurationManager.AppSettings["db"];
            Database db = server.Databases[dbName];
            List<SqlSmoObject> list = new List<SqlSmoObject>();
            DataTable dataTable = db.EnumObjects(DatabaseObjectTypes.UserDefinedFunction);
            foreach (DataRow row in dataTable.Rows)
            {
                int count = 0;
                string sSchema = (string)row["Schema"];
                if (sSchema == "sys" || sSchema == "INFORMATION_SCHEMA")
                    continue;

                UserDefinedFunction sp = (UserDefinedFunction)server.GetSmoObject(
                new Urn((string)row["Urn"]));
                if (!sp.IsSystemObject)
                {
                    string name = sp.Schema + '.' + sp.Name;

                    foreach (var item in spName)
                    {
                        if (item.Equals(name))
                        {
                            list.Add(sp);
                            count++;
                        }
                    }
                }

                for (int i = 0; i < count; i++)
                {
                    try
                    {
                        TextWriter tw = new StreamWriter(spFilePath + sp.Schema + '.' + sp.Name + ".txt", true);
                        tw.Write(sp.TextHeader);
                        tw.Write("\n");
                        tw.Write(sp.TextBody);
                        tw.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("hata var: " + ex);
                    }
                }
            }
            MessageBox.Show("spler kayıt edildi.", "Bilgilendirme Penceresi");

        }

        #endregion

        #region View
        private void btnView_Click(object sender, EventArgs e)
        {
            string spFilePath = ConfigurationManager.AppSettings["spFile"];
            string spNameFilePath = ConfigurationManager.AppSettings["spNameFile"];
            string filePath = @spNameFilePath;
            List<string> spName = File.ReadAllLines(filePath).ToList();

            Server server = new Server(serverKurulum());
            string dbName = ConfigurationManager.AppSettings["db"];
            Database db = server.Databases[dbName];
            List<SqlSmoObject> list = new List<SqlSmoObject>();
            DataTable dataTable = db.EnumObjects(DatabaseObjectTypes.View);
            foreach (DataRow row in dataTable.Rows)
            {
                int count = 0;
                string sSchema = (string)row["Schema"];
                if (sSchema == "sys" || sSchema == "INFORMATION_SCHEMA")
                    continue;

                View sp = (View)server.GetSmoObject(
                new Urn((string)row["Urn"]));
                if (!sp.IsSystemObject)
                {
                    string name = sp.Schema + '.' + sp.Name;

                    foreach (var item in spName)
                    {
                        if (item.Equals(name))
                        {
                            list.Add(sp);
                            count++;
                        }
                    }
                }

                for (int i = 0; i < count; i++)
                {
                    try
                    {
                        TextWriter tw = new StreamWriter(spFilePath + sp.Schema + '.' + sp.Name + ".txt", true);
                        tw.Write(sp.TextHeader);
                        tw.Write("\n");
                        tw.Write(sp.TextBody);
                        tw.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("hata var: " + ex);
                    }
                }
            }
            MessageBox.Show("spler kayıt edildi.", "Bilgilendirme Penceresi");

        }

        #endregion

        #region ListeKarsilastirma
        private void btnListKarsilastirma_Click(object sender, EventArgs e)
        {
            string filePath = ConfigurationManager.AppSettings["filePath"];
            string fileContent = @filePath;
            List<string> fileContentList = File.ReadAllLines(fileContent).ToList();

            string filePath2 = ConfigurationManager.AppSettings["filePath2"];

            string fileContent2 = @filePath2;
            List<string> fileContentList2 = File.ReadAllLines(fileContent2).ToList();

            var differenceBetweenList = fileContentList.Except(fileContentList2).ToList();

            string targetFile = ConfigurationManager.AppSettings["targetFile"];
            TextWriter tw = new StreamWriter(targetFile, true);

            for (int i = 0; i < differenceBetweenList.Count; i++)
            {
                tw.WriteLine(differenceBetweenList[i]);
            }
            tw.Close();
        }

        #endregion
    }

}