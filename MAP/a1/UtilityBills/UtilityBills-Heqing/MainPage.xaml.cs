using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace UtilityBills_Heqing
{
    public partial class MainPage : ContentPage
    {
        // cost rates of the electricity
        const double DAYTIME_RATE = 0.314;
        const double EVENING_RATE = 0.186;
        const double REBATE_RATE = 0.095;
        public MainPage()
        {
            InitializeComponent();
            ProvincePicker.SelectedIndexChanged += OnProvinceSelected;
        }

        void OnProvinceSelected(object sender, EventArgs e)
        {
            if (ProvincePicker.SelectedIndex == -1)
            {
                return;
            }
            //residents of British Columbia can only use renewable energy sources
            if (ProvincePicker.SelectedItem.ToString() == "British Columbia")
            {
                RenewableSwitch.IsToggled = true;  
                RenewableSwitch.IsEnabled = false;  
            }
            else
            {
                RenewableSwitch.IsEnabled = true;  
            }

        }

        void OnCalculateClicked(object sender, EventArgs e)
        {
            lblErr.Text = "";
            lblErr.IsVisible = false;
            //validate
            if (string.IsNullOrEmpty(DayUsageEntry.Text) || string.IsNullOrEmpty(EveningUsageEntry.Text) || ProvincePicker.SelectedIndex == -1)
            {
                lblErr.Text = "You must enter valid usage values.";
                lblErr.IsVisible = true;
                return;  
            }

            double dayUsage = double.Parse(DayUsageEntry.Text); 
            double eveningUsage = double.Parse(EveningUsageEntry.Text);

            // calculate electricity charge
            double dayCharge = dayUsage * DAYTIME_RATE;  
            double eveningCharge = eveningUsage * EVENING_RATE;  
            double totalUsageCharge = dayCharge + eveningCharge;

            // calculate sales tax
            double salesTaxRate = GetSalesTax(ProvincePicker.SelectedItem.ToString());
            double salesTax = totalUsageCharge * salesTaxRate;
            // calculate rebate 
            double rebate = RenewableSwitch.IsToggled ? totalUsageCharge * REBATE_RATE : 0;

            // calculate TOTAL Bill Amount
            double totalBill = totalUsageCharge + salesTax - rebate;

            // display electricity bill
            DayUsageChargeLabel.Text = $"Daytime Usage Charges: {dayCharge:C2}";
            EveningUsageChargeLabel.Text = $"Evening Usage Charges: {eveningCharge:C2}";
            TotalUsageChargeLabel.Text = $"Total Charges: {totalUsageCharge:C2}";
            SalesTaxLabel.Text = $"Sales Tax ({salesTaxRate * 100}%): {salesTax:C2}";
            RebateLabel.Text = $"Environmental Rebate: -{rebate:C2}";
            TotalBillLabel.Text = $"You Must Pay: {totalBill:C2}";

            ChargeBreakdownLabel.IsVisible = true;
            DayUsageChargeLabel.IsVisible = true;
            EveningUsageChargeLabel.IsVisible = true;
            TotalUsageChargeLabel.IsVisible = true;
            SalesTaxLabel.IsVisible = true;
            RebateLabel.IsVisible = true;
            TotalBillLabel.IsVisible = true;
        }
        double GetSalesTax(string province)
        {
            double taxRate = 0;  
            switch (province)
            {
                case "Alberta":
                    taxRate = 0;  
                    break;
                case "British Columbia":
                    taxRate = 0.12;  
                    break;
                case "Ontario":
                    taxRate = 0.13;  
                    break;
                case "Newfoundland and Labrador":
                    taxRate = 0.15;  
                    break;
            }
            return taxRate; 
        }
        void OnResetClicked(object sender, EventArgs e)
        {
            //set to default
            DayUsageEntry.Text = string.Empty;  
            EveningUsageEntry.Text = string.Empty;  
            ProvincePicker.SelectedIndex = -1;  
            RenewableSwitch.IsToggled = false;  // the switch is set to OFF
            BillResultLabel.IsVisible = false;

            if (lblErr != null) lblErr.IsVisible = false;
            if (ChargeBreakdownLabel != null) ChargeBreakdownLabel.IsVisible = false;
            if (DayUsageChargeLabel != null) DayUsageChargeLabel.IsVisible = false;
            if (EveningUsageChargeLabel != null) EveningUsageChargeLabel.IsVisible = false;
            if (TotalUsageChargeLabel != null) TotalUsageChargeLabel.IsVisible = false;
            if (SalesTaxLabel != null) SalesTaxLabel.IsVisible = false;
            if (RebateLabel != null) RebateLabel.IsVisible = false;
            if (TotalBillLabel != null) TotalBillLabel.IsVisible = false;
        }
    }
}
