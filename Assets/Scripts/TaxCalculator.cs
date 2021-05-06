﻿using UnityEngine;
using UnityEngine.UI;
using SpeechLib;


public class TaxCalculator : MonoBehaviour
{
    // Constant rate for the Medicare Levy
    const double MEDICARE_LEVY = 0.02;

    // Variables
    bool textToSpeechEnabled = true;
    public InputField grossSaleryInputField;
    public Dropdown saleryPayPirodDropDown;
    public Text netIncome;
    public Text medicareLevy;
    public Text incomeTaxPaid;

    private void Start()
    {
        Speak("Welcome to the A.T.O. Tax Calculator");
    }

    // Run this function on the click event of your 'Calculate' button
    public void Calculate()
    {
        // Initialisation of variables
        double medicareLevyPaid = 0;
        double incomeTaxPaid = 0;

        // Input
        double grossSalaryInput = GetGrossSalary();
        string salaryPayPeriod = GetSalaryPayPeriod();

        // Calculations
        double grossYearlySalary = CalculateGrossYearlySalary(grossSalaryInput, salaryPayPeriod);
        double netIncome = CalculateNetIncome(grossYearlySalary, ref medicareLevyPaid, ref incomeTaxPaid);

        // Output
        OutputResults(medicareLevyPaid, incomeTaxPaid, netIncome);
    }

    private double GetGrossSalary()
    {
        // Get from user. E.g. input box
        // Validate the input (ensure it is a positive, valid number)
        double grossYearlySalary = double.Parse(grossSaleryInputField.text);
        return grossYearlySalary;
    }

    private string GetSalaryPayPeriod()
    {
        {
            if (saleryPayPirodDropDown.value == 0) return "weekly";
            else if (saleryPayPirodDropDown.value == 1) return "fortnightly";
            else if (saleryPayPirodDropDown.value == 2) return "monthly";
            else if (saleryPayPirodDropDown.value == 3) return "quaterly";
            else return "semi annually";
        }
    }

    private double CalculateGrossYearlySalary(double grossSalaryInput, string salaryPayPeriod)
    {
        if (salaryPayPeriod == "weekly") return grossSalaryInput * 52;
        else if (salaryPayPeriod == "fortnightly") return grossSalaryInput * 26;
        else if (salaryPayPeriod == "monthly") return grossSalaryInput * 12;
        else return grossSalaryInput;
    }

    private double CalculateNetIncome(double grossYearlySalary, ref double medicareLevyPaid, ref double incomeTaxPaid)
    {
        double netIncome = (grossYearlySalary - medicareLevyPaid - incomeTaxPaid);
        return netIncome;
    }

    private double CalculateMedicareLevy(double grossYearlySalary)
    {
        double medicareLevyPaid = (grossYearlySalary * MEDICARE_LEVY);        
        return medicareLevyPaid;
    }

    private double CalculateIncomeTax(double grossYearlySalary)
    {
        if (grossYearlySalary <= 18200) return 0;
        else if (grossYearlySalary <= 18001) return (grossYearlySalary - 37000) * 0.19;
        else if (grossYearlySalary <= 37001) return (grossYearlySalary - 87000) * 0.325 + 3572;
        else if (grossYearlySalary <= 87001) return (grossYearlySalary - 180000) * 0.37 + 19822;
        else return (grossYearlySalary - 180000) * 0.45 + 54323;
    }

    private void OutputResults(double medicareLevyPaid, double incomeTaxPaid, double netIncome)
    {
        // Output the following to the GUI
        // "Medicare levy paid: $" + medicareLevyPaid.ToString("F2");
        // "Income tax paid: $" + incomeTaxPaid.ToString("F2");
        // "Net income: $" + netIncome.ToString("F2");
    }

    // Text to Speech
    private void Speak(string textToSpeak)
    {
        if(textToSpeechEnabled)
        {
            SpVoice voice = new SpVoice();
            voice.Speak(textToSpeak);
        }
    }
}
