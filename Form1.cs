using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HR_Control
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            testDBDataSet.DataSetName = "testDBDataSet";
            testDBDataSet.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;

            InitializeComponent();

            InitPositionsTable();
            InitUnitTable();
            InitStaffTable();
            InitStaffArchiveTable();
            InitStaffWithPositionAndUnitTable();
            InitStaff_Units_PositionsTable();

            Configure_Staff_DataGridView();
            Configure_FirePanel_DataGridView();
            Configure_StaffArchivePanel_DataGridView();
            Configure_TransferPanel_DataGridView();

            AddStafferPanel_TableSearchHandler();

            prevOpenedPanel = home_panel;
        }

        private static string connectionString = "Data Source=NORDMAN-COMP;Initial Catalog=testDB;User id=my_sa;Password=123";

        private SqlConnection testDBConnection = new SqlConnection(connectionString);

        private testDBDataSet testDBDataSet = new testDBDataSet();


        private testDBDataSetTableAdapters.PositionsTableAdapter positionsTableAdapter =
            new testDBDataSetTableAdapters.PositionsTableAdapter();
        
        private testDBDataSetTableAdapters.UnitsTableAdapter unitsTableAdapter = 
            new testDBDataSetTableAdapters.UnitsTableAdapter();

        private testDBDataSetTableAdapters.StaffTableAdapter staffTableAdapter = 
            new testDBDataSetTableAdapters.StaffTableAdapter();

        private testDBDataSetTableAdapters.GetArchiveStaffDataTableAdapter GetArchiveStaffDataViewTableAdapter =
            new testDBDataSetTableAdapters.GetArchiveStaffDataTableAdapter();

        private testDBDataSetTableAdapters.GetStaffWithPositionAndUnitTableAdapter staffWithPositionAndUnitTableAdapter =
            new testDBDataSetTableAdapters.GetStaffWithPositionAndUnitTableAdapter();

        private testDBDataSetTableAdapters.Staff_Units_PositionsTableAdapter Staff_Units_PositionsTableAdapter =
            new testDBDataSetTableAdapters.Staff_Units_PositionsTableAdapter();


        private Panel prevOpenedPanel;

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'testDBDataSet1.Units' table. You can move, or remove it, as needed.
            this.unitsTableAdapter1.Fill(this.testDBDataSet1.Units);
        }

        private void unitsBindingSource1BindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.unitsBindingSource1.EndEdit();
            this.tableAdapterManager.UpdateAll(this.testDBDataSet1);

        }

        // Tables initializers
        #region Tables initializers

        private void InitStaffTable()
        {
            staffTableAdapter.ClearBeforeFill = true;

            staffBindingSource.DataMember = "Staff";
            staffBindingSource.DataSource = testDBDataSet;

            staffTableAdapter.Fill(testDBDataSet.Staff);
        }

        private void InitPositionsTable()
        {
            positionsTableAdapter.ClearBeforeFill = true;

            positionsBindingSource.DataMember = "Positions";
            positionsBindingSource.DataSource = testDBDataSet;

            positionsTableAdapter.Fill(testDBDataSet.Positions);

            positionsDataGridView.DataSource = positionsBindingSource;
            positionsDataGridView.Columns["ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            positionsDataGridView.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            positionsDataGridView.Columns["Salary"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            positionsDataGridView.Columns["Premium"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void InitUnitTable()
        {
            unitsTableAdapter.ClearBeforeFill = true;

            unitsBindingSource.DataMember = "Units";
            unitsBindingSource.DataSource = testDBDataSet;

            unitsTableAdapter.Fill(this.testDBDataSet.Units);

            unitsDataGridView.DataSource = unitsBindingSource;
            unitsDataGridView.Columns["ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            unitsDataGridView.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void InitStaffArchiveTable()
        {
            GetArchiveStaffDataViewTableAdapter.ClearBeforeFill = true;

            staffArchiveBindingSource.DataMember = "GetArchiveStaffData";
            staffArchiveBindingSource.DataSource = testDBDataSet;

            GetArchiveStaffDataViewTableAdapter.Fill(testDBDataSet.GetArchiveStaffData);
        }

        private void InitStaffWithPositionAndUnitTable()
        {
            staffWithPositionAndUnitTableAdapter.ClearBeforeFill = true;

            staffWithPositionAndUnitBindingSource.DataMember = "GetStaffWithPositionAndUnit";
            staffWithPositionAndUnitBindingSource.DataSource = testDBDataSet;

            staffWithPositionAndUnitTableAdapter.Fill(testDBDataSet.GetStaffWithPositionAndUnit);
        }

        private void InitStaff_Units_PositionsTable()
        {
            Staff_Units_PositionsTableAdapter.ClearBeforeFill = true;

            Staff_Units_PositionsTableAdapter.Fill(testDBDataSet.Staff_Units_Positions);
        }

        #endregion 


        // DataGridViews Configs
        #region DataGridViews Configs

        private void Configure_Staff_DataGridView()
        {
            addStafferPanel_dataGridView.DataSource = staffBindingSource;
            addStafferPanel_dataGridView.Columns["FirstName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            addStafferPanel_dataGridView.Columns["FirstName"].HeaderText = "Имя";
            addStafferPanel_dataGridView.Columns["SecondName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            addStafferPanel_dataGridView.Columns["SecondName"].HeaderText = "Фамилия";
            addStafferPanel_dataGridView.Columns["Patronymic"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            addStafferPanel_dataGridView.Columns["Patronymic"].HeaderText = "Отчество";
            addStafferPanel_dataGridView.Columns["PassportSeries"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            addStafferPanel_dataGridView.Columns["PassportSeries"].HeaderText = "Серия паспорта";
            addStafferPanel_dataGridView.Columns["PassportNumber"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            addStafferPanel_dataGridView.Columns["PassportNumber"].HeaderText = "Номер паспорта";
            addStafferPanel_dataGridView.Columns["BirthDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            addStafferPanel_dataGridView.Columns["BirthDate"].HeaderText = "Дата рождения";
            addStafferPanel_dataGridView.Columns["Education"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            addStafferPanel_dataGridView.Columns["Education"].HeaderText = "Образование";
            addStafferPanel_dataGridView.Columns["HireDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            addStafferPanel_dataGridView.Columns["HireDate"].HeaderText = "Дата приема";
        }

        private void Configure_FirePanel_DataGridView()
        {
            firePanel_dataGridView.DataSource = staffBindingSource;
            firePanel_dataGridView.Columns["FirstName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            firePanel_dataGridView.Columns["FirstName"].HeaderText = "Имя";
            firePanel_dataGridView.Columns["SecondName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            firePanel_dataGridView.Columns["SecondName"].HeaderText = "Фамилия";
            firePanel_dataGridView.Columns["Patronymic"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            firePanel_dataGridView.Columns["Patronymic"].HeaderText = "Отчество";
            firePanel_dataGridView.Columns["PassportSeries"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            firePanel_dataGridView.Columns["PassportSeries"].HeaderText = "Серия паспорта";
            firePanel_dataGridView.Columns["PassportNumber"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            firePanel_dataGridView.Columns["PassportNumber"].HeaderText = "Номер паспорта";
            firePanel_dataGridView.Columns["BirthDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            firePanel_dataGridView.Columns["BirthDate"].HeaderText = "Дата рождения";
            firePanel_dataGridView.Columns["Education"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            firePanel_dataGridView.Columns["Education"].HeaderText = "Образование";
            firePanel_dataGridView.Columns["HireDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            firePanel_dataGridView.Columns["HireDate"].HeaderText = "Дата приема";
        }

        private void Configure_TransferPanel_DataGridView()
        {
            transferStafferPanel_dataGridView.DataSource = staffWithPositionAndUnitBindingSource;
        }

        private void Configure_StaffArchivePanel_DataGridView()
        {
            staffArchive_dataGridView.DataSource = staffArchiveBindingSource;
            staffArchive_dataGridView.Columns["FirstName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            staffArchive_dataGridView.Columns["FirstName"].HeaderText = "Имя";
            staffArchive_dataGridView.Columns["SecondName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            staffArchive_dataGridView.Columns["SecondName"].HeaderText = "Фамилия";
            staffArchive_dataGridView.Columns["Patronymic"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            staffArchive_dataGridView.Columns["Patronymic"].HeaderText = "Отчество";
            staffArchive_dataGridView.Columns["PassportSeries"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            staffArchive_dataGridView.Columns["PassportSeries"].HeaderText = "Серия паспорта";
            staffArchive_dataGridView.Columns["PassportNumber"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            staffArchive_dataGridView.Columns["PassportNumber"].HeaderText = "Номер паспорта";
            staffArchive_dataGridView.Columns["BirthDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            staffArchive_dataGridView.Columns["BirthDate"].HeaderText = "Дата рождения";
            staffArchive_dataGridView.Columns["Education"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            staffArchive_dataGridView.Columns["Education"].HeaderText = "Образование";
            staffArchive_dataGridView.Columns["HireDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            staffArchive_dataGridView.Columns["HireDate"].HeaderText = "Дата приема";
            staffArchive_dataGridView.Columns["UnitName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            staffArchive_dataGridView.Columns["UnitName"].HeaderText = "Отдел";
            staffArchive_dataGridView.Columns["PositionName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            staffArchive_dataGridView.Columns["PositionName"].HeaderText = "Должность";
        }

        #endregion


        private void AnyBackButtonHandler()
        {
            prevOpenedPanel.Visible = false;
            main_panel.Visible = false;
            home_panel.Visible = true;
            prevOpenedPanel = home_panel;
        }

        #region Back and Ready buttons handlers

        private void addStaffer_back_button_Click(object sender, EventArgs e)
        {
            AnyBackButtonHandler();
        }

        private void addUnitPanel_back_button_Click(object sender, EventArgs e)
        {
            AnyBackButtonHandler();
        }

        private void addUnitPanel_ready_button_Click(object sender, EventArgs e)
        {
            AnyBackButtonHandler();
        }

        private void addPositionPanel_back_button_Click(object sender, EventArgs e)
        {
            AnyBackButtonHandler();
        }

        private void addPositionPanel_ready_button_Click(object sender, EventArgs e)
        {
            AnyBackButtonHandler();
        }

        private void addStafferPanel_ready_button_Click(object sender, EventArgs e)
        {
            AnyBackButtonHandler();
        }

        #endregion


        // Buttons click handlers
        #region Buttons click handlers

        private void startWork_button_Click(object sender, EventArgs e)
        {
            prevOpenedPanel.Visible = false;
            main_panel.Visible = true;
            addStaffer_panel.Visible = true;
            prevOpenedPanel = addStaffer_panel;
        }

        private void addStaffer_button_Click_1(object sender, EventArgs e)
        {
            using (SqlCommand cmdStaff = new SqlCommand())
            using (SqlCommand cmdStaff_Units_Positions = new SqlCommand())
            using (SqlCommand cmdStaff_Units = new SqlCommand())
            {
                Set_Staff_Command(cmdStaff);
                Set_Staff_Units_Positions_Command(cmdStaff_Units_Positions);
                //Set_Staff_Units_Command(cmdStaff_Units);

                testDBConnection.Open();

                cmdStaff.ExecuteNonQuery();
                cmdStaff_Units_Positions.ExecuteNonQuery();
                //cmdStaff_Units.ExecuteNonQuery();

                staffTableAdapter.Update(testDBDataSet.Staff);
                staffTableAdapter.Fill(testDBDataSet.Staff);

                testDBConnection.Close();

                ClearAddStafferTextBoxes();

                var myThread = new Thread(() =>
                {
                    Thread.Sleep(1500);
                    addStaffer_saved_label.Visible = false;
                });

                addStaffer_saved_label.Visible = true;
                myThread.Start();
            }
        }

        private void addPositionPanel_add_button_Click(object sender, EventArgs e)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = testDBConnection;

                cmd.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = positionName_textBox.Text;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@salaryAmount",
                    SqlDbType = SqlDbType.Decimal,
                    Precision = 10,
                    Scale = 2,
                    SourceColumn = "Salary"
                }).Value = decimal.Parse(salaryAmount_textBox.Text, System.Globalization.NumberStyles.Number);
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@premiumAmount",
                    SqlDbType = SqlDbType.Decimal,
                    Precision = 10,
                    Scale = 2,
                    SourceColumn = "Premium"
                }).Value = decimal.Parse(premiumAmount_textBox.Text,System.Globalization.NumberStyles.Number);

                cmd.CommandText = "insert into Positions " +
                    "(Name, Salary, Premium) " +
                    "values (@name, @salaryAmount, @premiumAmount)";

                testDBConnection.Open();
                cmd.ExecuteNonQuery();

                positionsTableAdapter.Update(testDBDataSet.Positions);
                positionsTableAdapter.Fill(testDBDataSet.Positions);

                testDBConnection.Close();

                var myThread = new Thread(() =>
                {
                    Thread.Sleep(1500);
                    addPositionPanel_saved_label.Visible = false;
                });

                addPositionPanel_saved_label.Visible = true;
                myThread.Start();
            }
        }

        private void addUnitPanel_add_button_Click(object sender, EventArgs e)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = testDBConnection;

                cmd.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = unitName_textBox.Text;

                cmd.CommandText = "insert into Units " +
                    "(Name) " + "values (@name)";

                testDBConnection.Open();
                cmd.ExecuteNonQuery();
                testDBConnection.Close();

                unitsTableAdapter.Update(this.testDBDataSet.Units);
                unitsTableAdapter.Fill(this.testDBDataSet.Units);

                var myThread = new Thread(() =>
                {
                    Thread.Sleep(1500);
                    addUnitPanel_saved_label.Visible = false;
                    
                });

                addUnitPanel_saved_label.Visible = true;
                myThread.Start();
            }
        }

        private void firePanel_fire_button_Click(object sender, EventArgs e)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = testDBConnection;

                cmd.Parameters.Add("@passportSeries", SqlDbType.NVarChar, 4).Value = firePanel_passportSeries_textBox.Text;
                cmd.Parameters.Add("@passportNumber", SqlDbType.NVarChar, 6).Value = firePanel_passportNumber_textBox.Text;

                cmd.CommandText = "delete from Staff " +
                    "where PassportSeries = @passportSeries and PassportNumber = @passportNumber";

                testDBConnection.Open();
                cmd.ExecuteNonQuery();
                testDBConnection.Close();

                staffTableAdapter.Update(testDBDataSet.Staff);
                staffTableAdapter.Fill(testDBDataSet.Staff);

                firePanel_dataGridView.DataSource = staffBindingSource;

                ClearFireStafferTextBoxes();

                var myThread = new Thread(() =>
                {
                    Thread.Sleep(1500);
                    firePanel_saved_label.Visible = false;

                });

                firePanel_saved_label.Visible = true;
                myThread.Start();
            }
        }

        private void transferStafferPanel_transfer_button_Click(object sender, EventArgs e)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = testDBConnection;

                cmd.Parameters.Add("@passportSeries", SqlDbType.NVarChar, 4).Value =
                    transferStafferPanel_passportSeries_textBox.Text;
                cmd.Parameters.Add("@passportNumber", SqlDbType.NVarChar, 6).Value =
                    transferStafferPanel_passportNumber_textBox.Text;
                cmd.Parameters.Add("@positionId", SqlDbType.Int).Value = 
                    int.Parse(transferStafferPanel_position_comboBox.SelectedValue.ToString());

                cmd.CommandText = "update Staff_Units_Positions set PositionID = @positionId " +
                    "where PassportSeries = @passportSeries and PassportNumber = @passportNumber";

                testDBConnection.Open();
                cmd.ExecuteNonQuery();
                testDBConnection.Close();

                Staff_Units_PositionsTableAdapter.Update(testDBDataSet.Staff_Units_Positions);
                staffWithPositionAndUnitTableAdapter.Fill(this.testDBDataSet.GetStaffWithPositionAndUnit);

                var myThread = new Thread(() =>
                {
                    Thread.Sleep(1500);
                    transferStafferPanel_saved_label.Visible = false;

                });

                transferStafferPanel_saved_label.Visible = true;
                myThread.Start();
            }
        }

        private void Set_Staff_Command(SqlCommand cmdStaff)
        {
            cmdStaff.Connection = testDBConnection;

            cmdStaff.Parameters.Add("@firstName", SqlDbType.NVarChar, 30).Value = addStafferPanel_firstName_textBox.Text;
            cmdStaff.Parameters.Add("@secondName", SqlDbType.NVarChar, 30).Value = addStafferPanel_secondName_textBox.Text;
            cmdStaff.Parameters.Add("@patronymic", SqlDbType.NVarChar, 30).Value = addStafferPanel_patronymic_textBox.Text;
            cmdStaff.Parameters.Add("@passportSeries", SqlDbType.NVarChar, 4).Value = addStafferPanel_passportSeries_textBox.Text;
            cmdStaff.Parameters.Add("@passportNumber", SqlDbType.NVarChar, 6).Value = addStafferPanel_passportNumber_textBox.Text;
            cmdStaff.Parameters.Add("@birthDate", SqlDbType.Date).Value = addStafferPanel_birthDate_dateTimePicker.Value.Date;
            cmdStaff.Parameters.Add("@education", SqlDbType.NVarChar, 50).Value = addStafferPanel_education_textBox.Text;
            cmdStaff.Parameters.Add("@hireDate", SqlDbType.Date).Value = addStafferPanel_hireDate_dateTimePicker.Value.Date;

            cmdStaff.CommandText = "insert into Staff " +
                "(FirstName, SecondName, Patronymic, PassportSeries, PassportNumber, BirthDate, Education, HireDate) " +
                "values (@firstName, @secondName, @patronymic, @passportSeries," +
                "@passportNumber, @birthDate, @education, @hireDate)";
        }

        private void Set_Staff_Units_Positions_Command(SqlCommand cmdStaff_Units_Positions)
        {
            cmdStaff_Units_Positions.Connection = testDBConnection;

            cmdStaff_Units_Positions.Parameters.Add("@passportSeries", SqlDbType.NVarChar, 4).Value =
                    addStafferPanel_passportSeries_textBox.Text;
            cmdStaff_Units_Positions.Parameters.Add("@passportNumber", SqlDbType.NVarChar, 6).Value =
                addStafferPanel_passportNumber_textBox.Text;
            cmdStaff_Units_Positions.Parameters.Add("@unitID", SqlDbType.Int).Value =
                int.Parse(addStafferPanel_unit_comboBox.SelectedValue.ToString());
            cmdStaff_Units_Positions.Parameters.Add("@positionID", SqlDbType.Int).Value =
                int.Parse(addStafferPanel_position_comboBox.SelectedValue.ToString());

            cmdStaff_Units_Positions.CommandText = "insert into Staff_Units_Positions" +
                "(PassportSeries, PassportNumber, UnitID, PositionID)" +
                "values (@passportSeries, @passportNumber, @unitID, @positionID)";
        }

        #endregion

        // Clear TextBoxes code
        #region Clear TextBoxes code

        private void ClearAddStafferTextBoxes()
        {
            addStafferPanel_firstName_textBox.Clear();
            addStafferPanel_secondName_textBox.Clear();
            addStafferPanel_patronymic_textBox.Clear();
            addStafferPanel_passportSeries_textBox.Clear();
            addStafferPanel_passportNumber_textBox.Clear();
            addStafferPanel_education_textBox.Clear();
        }

        private void ClearFireStafferTextBoxes()
        {
            firePanel_firstName_textBox.Clear();
            firePanel_secondName_textBox.Clear();
            firePanel_patronymic_textBox.Clear();
            firePanel_passportSeries_textBox.Clear();
            firePanel_passportNumber_textBox.Clear();
        }

        #endregion

        // Search rows in tables
        #region Search rows in tables code

        private Thread staffTableSearchThread;
        private Thread staffWith_P_and_U_TableSearchThread;

        private DataTable addStafferPanelSearchTable = new DataTable();
        private DataTable fireStafferPanelSearchTable = new DataTable();
        private DataTable transferStafferPanelSearchTable = new DataTable();

        private void InitSearchInStaffTableThread(string firstName, string secondName, string patronymic,
            string passportSeries, string passportNumber, DataTable searchTable, DataGridView dataGridView,
            BindingSource bindingSource)
        {
            staffTableSearchThread = new Thread(() =>
            {
                Thread.Sleep(1000);

                var searchString = "";

                if (secondName != "")
                {
                    if (searchString != "")
                        searchString += "and ";
                    searchString = "SecondName like '" + secondName + "%' ";
                }
                if (firstName != "")
                {
                    if (searchString != "")
                        searchString += "and ";
                    searchString += "FirstName like '" + firstName + "%' ";
                }
                if (patronymic != "")
                {
                    if (searchString != "")
                        searchString += "and ";
                    searchString += "Patronymic like '" + patronymic + "%' ";
                }
                if (passportSeries != "")
                {
                    if (searchString != "")
                        searchString += "and ";
                    searchString += "PassportSeries like '" + passportSeries + "%' ";
                }
                if (passportNumber != "")
                {
                    if (searchString != "")
                        searchString += "and ";
                    searchString += "PassportNumber like '" + passportNumber + "%' ";
                }

                var dataRows = testDBDataSet.Staff.Select(searchString);

                if (dataRows.Count() > 0)
                {
                    searchTable = dataRows.CopyToDataTable();
                    this.Invoke((MethodInvoker)delegate
                    {
                        dataGridView.DataSource = searchTable;
                        dataGridView.Update();
                    });
                }

                else
                {
                    searchTable.Clear();
                    this.Invoke((MethodInvoker)delegate
                    {
                        dataGridView.DataSource = bindingSource;
                        dataGridView.Update();
                    });
                }
                
            });
        }

        private void InitSearchInStaffWith_P_and_U_TableThread(string firstName, string secondName, string patronymic,
            string passportSeries, string passportNumber, DataTable searchTable, DataGridView dataGridView,
            BindingSource bindingSource)
        {
            staffWith_P_and_U_TableSearchThread = new Thread(() =>
            {
                Thread.Sleep(1000);

                var searchString = "";

                if (secondName != "")
                {
                    if (searchString != "")
                        searchString += "and ";
                    searchString = "Фамилия like '" + secondName + "%' ";
                }
                if (firstName != "")
                {
                    if (searchString != "")
                        searchString += "and ";
                    searchString += "Имя like '" + firstName + "%' ";
                }
                if (patronymic != "")
                {
                    if (searchString != "")
                        searchString += "and ";
                    searchString += "Отчествов like '" + patronymic + "%' ";
                }
                if (passportSeries != "")
                {
                    if (searchString != "")
                        searchString += "and ";
                    searchString += "[Серия паспорта] like '" + passportSeries + "%' ";
                }
                if (passportNumber != "")
                {
                    if (searchString != "")
                        searchString += "and ";
                    searchString += "[Номер паспорта] like '" + passportNumber + "%' ";
                }

                var dataRows = testDBDataSet.GetStaffWithPositionAndUnit.Select(searchString);

                if (dataRows.Count() > 0)
                {
                    searchTable = dataRows.CopyToDataTable();
                    this.Invoke((MethodInvoker)delegate
                    {
                        dataGridView.DataSource = searchTable;
                        dataGridView.Update();
                    });
                }

                else
                {
                    searchTable.Clear();
                    this.Invoke((MethodInvoker)delegate
                    {
                        dataGridView.DataSource = bindingSource;
                        dataGridView.Update();
                    });
                }

            });
        }

        private void AddStafferPanel_TableSearchHandler()
        {
            if (staffTableSearchThread != null && staffTableSearchThread.IsAlive) staffTableSearchThread.Abort();
            InitSearchInStaffTableThread(
                addStafferPanel_firstName_textBox.Text,
                addStafferPanel_secondName_textBox.Text,
                addStafferPanel_patronymic_textBox.Text,
                addStafferPanel_passportSeries_textBox.Text,
                addStafferPanel_passportNumber_textBox.Text,
                addStafferPanelSearchTable,
                addStafferPanel_dataGridView,
                staffBindingSource
                );
            staffTableSearchThread.Start();
        }

        private void FireStafferPanel_TableSearchHandler()
        {
            if (staffTableSearchThread != null && staffTableSearchThread.IsAlive) staffTableSearchThread.Abort();
            InitSearchInStaffTableThread(
                firePanel_firstName_textBox.Text,
                firePanel_secondName_textBox.Text,
                firePanel_patronymic_textBox.Text,
                firePanel_passportSeries_textBox.Text,
                firePanel_passportNumber_textBox.Text,
                fireStafferPanelSearchTable,
                firePanel_dataGridView,
                staffBindingSource
                );
            staffTableSearchThread.Start();
        }

        private void TransferStafferPanel_TableSearchHandler()
        {
            if (staffWith_P_and_U_TableSearchThread != null && staffWith_P_and_U_TableSearchThread.IsAlive)
                staffWith_P_and_U_TableSearchThread.Abort();
            InitSearchInStaffWith_P_and_U_TableThread(
                transferStafferPanel_firstName_textBox.Text,
                transferStafferPanel_secondName_textBox.Text,
                transferStafferPanel_patronymic_textBox.Text,
                transferStafferPanel_passportSeries_textBox.Text,
                transferStafferPanel_passportNumber_textBox.Text,
                transferStafferPanelSearchTable,
                transferStafferPanel_dataGridView,
                staffWithPositionAndUnitBindingSource
                );
            staffWith_P_and_U_TableSearchThread.Start();
        }

        #endregion


        // ToolStripMenu click handlers
        #region ToolStripMenu click handlers

        private void addStaffer_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePanelVisibility(addStaffer_panel);
        }

        private void fireStaffer_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePanelVisibility(fireStaffer_panel);
        }

        private void addUnit_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePanelVisibility(addUnit_panel);
        }

        private void addPosition_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePanelVisibility(addPosition_panel);
        }

        private void transferStaffer_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePanelVisibility(transferStafferPanel);
            staffWithPositionAndUnitTableAdapter.Fill(this.testDBDataSet.GetStaffWithPositionAndUnit);
        }

        private void staffArchiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePanelVisibility(staffArchive_panel);
        }

        private void ChangePanelVisibility(Panel panelToShow)
        {
            prevOpenedPanel.Visible = false;
            panelToShow.Visible = true;
            prevOpenedPanel = panelToShow;
        }

        #endregion


        // DataGridViews SelectionChanged handlers
        #region DataGridViews SelectionChanged handlers

        private void FirePanel_dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (firePanel_dataGridView.SelectedCells.Count > 0 && firePanel_fillTextBoxes_checkcBox.Checked)
            {
                firePanel_firstName_textBox.Text =
                    firePanel_dataGridView.SelectedCells[0].OwningRow.Cells["FirstName"].Value.ToString();
                firePanel_secondName_textBox.Text =
                    firePanel_dataGridView.SelectedCells[0].OwningRow.Cells["SecondName"].Value.ToString();
                firePanel_patronymic_textBox.Text =
                    firePanel_dataGridView.SelectedCells[0].OwningRow.Cells["Patronymic"].Value.ToString();

                firePanel_passportSeries_textBox.Text =
                    firePanel_dataGridView.SelectedCells[0].OwningRow.Cells["PassportSeries"].Value.ToString();
                firePanel_passportNumber_textBox.Text =
                    firePanel_dataGridView.SelectedCells[0].OwningRow.Cells["PassportNumber"].Value.ToString();
            }
        }

        private void transferStafferPanel_dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (transferStafferPanel_dataGridView.SelectedCells.Count > 0 && transferStafferPanel_fillTextBoxes_checkBox.Checked)
            {
                transferStafferPanel_firstName_textBox.Text =
                    transferStafferPanel_dataGridView.SelectedCells[0].OwningRow.Cells["Имя"].Value.ToString();
                transferStafferPanel_secondName_textBox.Text =
                    transferStafferPanel_dataGridView.SelectedCells[0].OwningRow.Cells["Фамилия"].Value.ToString();
                transferStafferPanel_patronymic_textBox.Text =
                    transferStafferPanel_dataGridView.SelectedCells[0].OwningRow.Cells["Отчествов"].Value.ToString();

                transferStafferPanel_passportSeries_textBox.Text =
                    transferStafferPanel_dataGridView.SelectedCells[0].OwningRow.Cells["Серия паспорта"].Value.ToString();
                transferStafferPanel_passportNumber_textBox.Text =
                    transferStafferPanel_dataGridView.SelectedCells[0].OwningRow.Cells["Номер паспорта"].Value.ToString();

                transferStafferPanel_position_comboBox.Text =
                    transferStafferPanel_dataGridView.SelectedCells[0].OwningRow.Cells["Должность"].Value.ToString();
            }
        }

        #endregion


        // CheckBoxes handlers
        #region CheckBoxes handlers

        private void FirePanel_fillTextBoxes_checkcBox_CheckedChanged(object sender, EventArgs e)
        {
            if (firePanel_fillTextBoxes_checkcBox.Checked)
            {
                FirePanel_dataGridView_SelectionChanged(this, new EventArgs());
            }
            else
            {
                firePanel_firstName_textBox.Clear();
                firePanel_secondName_textBox.Clear();
                firePanel_patronymic_textBox.Clear();

                firePanel_passportSeries_textBox.Clear();
                firePanel_passportNumber_textBox.Clear();
            }
        }

        private void transferStafferPanel_fillTextBoxes_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (transferStafferPanel_fillTextBoxes_checkBox.Checked)
            {
                transferStafferPanel_dataGridView_SelectionChanged(this, new EventArgs());
            }
            else
            {
                transferStafferPanel_firstName_textBox.Clear();
                transferStafferPanel_secondName_textBox.Clear();
                transferStafferPanel_patronymic_textBox.Clear();

                transferStafferPanel_passportSeries_textBox.Clear();
                transferStafferPanel_passportNumber_textBox.Clear();
            }
        }

        #endregion


        // TextBoxes TextChanged Handlers
        #region TextBoxes TextChanged Handlers

        private void secondName_textBox_TextChanged(object sender, EventArgs e)
        {
            AddStafferPanel_TableSearchHandler();
        }

        private void firstName_textBox_TextChanged(object sender, EventArgs e)
        {
            AddStafferPanel_TableSearchHandler();
        }

        private void patronymic_textBox_TextChanged(object sender, EventArgs e)
        {
            AddStafferPanel_TableSearchHandler();
        }

        private void passportSeries_textBox_TextChanged(object sender, EventArgs e)
        {
            AddStafferPanel_TableSearchHandler();
        }

        private void passportNumber_textBox_TextChanged(object sender, EventArgs e)
        {
            AddStafferPanel_TableSearchHandler();
        }

        private void firePanel_secondName_textBox_TextChanged(object sender, EventArgs e)
        {
            FireStafferPanel_TableSearchHandler();
        }

        private void firePanel_firstName_textBox_TextChanged(object sender, EventArgs e)
        {
            FireStafferPanel_TableSearchHandler();
        }

        private void firePanel_patronymic_textBox_TextChanged(object sender, EventArgs e)
        {
            FireStafferPanel_TableSearchHandler();
        }

        private void firePanel_passportSeries_textBox_TextChanged(object sender, EventArgs e)
        {
            FireStafferPanel_TableSearchHandler();
        }

        private void firePanel_passportNumber_textBox_TextChanged(object sender, EventArgs e)
        {
            FireStafferPanel_TableSearchHandler();
        }

        private void transferStafferPanel_secondName_textBox_TextChanged(object sender, EventArgs e)
        {
            TransferStafferPanel_TableSearchHandler();
        }

        private void transferStafferPanel_firstName_textBox_TextChanged(object sender, EventArgs e)
        {
            TransferStafferPanel_TableSearchHandler();
        }

        private void transferStafferPanel_patronymic_textBox_TextChanged(object sender, EventArgs e)
        {
            TransferStafferPanel_TableSearchHandler();
        }

        private void transferStafferPanel_passportSeries_textBox_TextChanged(object sender, EventArgs e)
        {
            TransferStafferPanel_TableSearchHandler();
        }

        private void transferStafferPanel_passportNumber_textBox_TextChanged(object sender, EventArgs e)
        {
            TransferStafferPanel_TableSearchHandler();
        }

        #endregion
    }
}
