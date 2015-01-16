﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using GraphCaller;

namespace ExcelAddIn1
{
    public partial class Ribbon1
    {
        private string token = "CAACZBwbJIzL0BAIGWuGP5lqZA3gHLckbs8B5LGfD3mTxpxTZBguPjrgi3KnAAsLsZAOOuC0PVPyGnku8iHMsZCZCKj0JBQgYdOe3kZAAiptX56tfqcytjZCc8KvJ64gmXlZCZCXoelU9SrgmzK0IuirbV2KtXg2n13e8QRXTuwITt57ZCSzNdASpKdp";
                
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {
            // id, status, name, time updated
        }

        private void syncButton_Click(object sender, RibbonControlEventArgs e)
        {
            Excel.Window window = e.Control.Context;
            Excel.Worksheet activeWorksheet = ((Excel.Worksheet)window.Application.ActiveSheet);
            string account = this.accountNoEdit.Text;
            for (int i = 1; i <= 1; i++)
            {
                Excel.Range cell = activeWorksheet.get_Range("A" + i);
                string adgroupId = "";
                if (cell.Value2 is Double) {
                    adgroupId = cell.Value2.ToString();
                }
                string adgroupName = activeWorksheet.get_Range("B" + i).Value2;
                if (!adgroupId.Equals(""))
                {
                    Uploader.Edit(adgroupId, token,
                      new Dictionary<string, object>() {
                        {"name", adgroupName}}
                    );
                }
            }
            MessageBox.Show("Data successfully synced!");
        }

        private async void loadButton_Click(object sender, RibbonControlEventArgs e)
        {

            long account;
            List<AdGroups> adgroups;
            GetAdgroups(out account, out adgroups);

            Excel.Window window = e.Control.Context;
            Excel.Worksheet activeWorksheet = ((Excel.Worksheet)window.Application.ActiveSheet);
            int i = 1;
            foreach (var adgroup in adgroups)
            {
                Excel.Range adgroupId = activeWorksheet.get_Range("A" + i);
                adgroupId.Value2 = adgroup.AdgroupId;
                //adgroupId.set_Item(adgroup.AdgroupId, typeof(long));
                Excel.Range adgroupName = activeWorksheet.get_Range("B" + i);
                adgroupName.Value2 = adgroup.AdgroupName;

                Excel.Range adgroupStatus = activeWorksheet.get_Range("C" + i);
                adgroupStatus.Value2 = adgroup.Status;

                i++;
            }

            var client = new ServiceReference1.Service1Client();
            FBAdAccount accountDetails = await client.GetAdAccountAsync(token, account);

            Excel.Range accountIdDesc = activeWorksheet.get_Range("L1");
            accountIdDesc.Value2 = "Account ID";
            Excel.Range accountId = activeWorksheet.get_Range("M1");
            accountId.Value2 = accountDetails.AccountId;

            Excel.Range accountdStatusDesc = activeWorksheet.get_Range("L2");
            accountdStatusDesc.Value2 = "Account Status";
            Excel.Range accountStatus = activeWorksheet.get_Range("M2");
            accountStatus.Value2 = accountDetails.AccountStatus;

            Excel.Range ageDesc = activeWorksheet.get_Range("L3");
            ageDesc.Value2 = "Age";
            Excel.Range age = activeWorksheet.get_Range("M3");
            age.Value2 = accountDetails.Age;

            Excel.Range currencyDesc = activeWorksheet.get_Range("L4");
            currencyDesc.Value2 = "Currency";
            Excel.Range currency = activeWorksheet.get_Range("M4");
            currency.Value2 = accountDetails.Currency;

            Excel.Range nameDesc = activeWorksheet.get_Range("L5");
            nameDesc.Value2 = "Name";
            Excel.Range name = activeWorksheet.get_Range("M5");
            name.Value2 = accountDetails.Name;

            Excel.Range businessCityDesc = activeWorksheet.get_Range("L6");
            businessCityDesc.Value2 = "Business City";
            Excel.Range businessCity = activeWorksheet.get_Range("M6");
            businessCity.Value2 = accountDetails.BusinessCity;

            Excel.Range spendCapDesc = activeWorksheet.get_Range("L7");
            spendCapDesc.Value2 = "Spend Cap";
            Excel.Range spendCap = activeWorksheet.get_Range("M7");
            spendCap.Value2 = accountDetails.SpendCap;

            //Excel.Range firstRow = activeWorksheet.get_Range("A1");
            //firstRow.Value2 = "New text";
            //firstRow.EntireRow.Insert(Excel.XlInsertShiftDirection.xlShiftDown);
            //Excel.Range newFirstRow = activeWorksheet.get_Range("A1");
            //newFirstRow.Value2 = "This text was added by using code";
        }

        private void GetAdgroups(out long account, out List<AdGroups> adgroups)
        {
            account = 10151318637546538;
            FieldsSelector selector = new FieldsSelector(account, token);
            selector.Text  = "Loading data for account: " + this.accountNoEdit.Text;
            var result = selector.ShowDialog();
            if (result == DialogResult.OK)
            {
                //var fields = FieldsSelector.selectedFields;
                //adgroups = AdGroups.getAdGroup(token, account, fields);
                adgroups = FieldsSelector.Adgroups;
            }
            else {
                adgroups = new List<AdGroups>(); // empty
            }
        }

        private void accountNoEdit_TextChanged(object sender, RibbonControlEventArgs e)
        {

        }

    }
}
