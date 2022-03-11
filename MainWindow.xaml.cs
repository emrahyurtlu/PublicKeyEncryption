using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace PublicKeyEncryption;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public List<int> EnChars = new();

    public List<int> MessageInIntegers = new();

    public MainWindow()
    {
        InitializeComponent();
    }

    public int P { get; set; }
    public int Q { get; set; }
    public int N { get; set; }
    public int Z { get; set; }
    public int E { get; set; }
    public int D { get; set; }

    public string CipheredTextInInt { get; set; }
    public string CipheredTextInHex { get; set; }

    public string PlainText { get; set; }


    private void calculateNandZbutton_Click(object sender, RoutedEventArgs e)
    {
        P = int.Parse(TB_P.Text);
        Q = int.Parse(TB_Q.Text);
        N = CalculateN();
        Z = CalculateZ();
        TB_N.Text = N.ToString();
        TB_Z.Text = Z.ToString();
    }

    private int CalculateZ()
    {
        return (P - 1) * (Q - 1);
    }

    private int CalculateN()
    {
        return P * Q;
    }

    private void Btn_CalculateKeys_Click(object sender, RoutedEventArgs e)
    {
        E = int.Parse(TB_E.Text);
        D = int.Parse(TB_D.Text);

        TB_SHOW_PUBLIC_E.Text = E.ToString();
        TB_SHOW_PUBLIC_N.Text = N.ToString();
        TB_SHOW_PRIVATE_N.Text = N.ToString();
        TB_SHOW_PRIVATE_D.Text = D.ToString();
    }

    private void Btn_Clear_Click(object sender, RoutedEventArgs e)
    {
        P = Q = N = Z = E = 0;
        TB_Z.Text = TB_N.Text = TB_Q.Text = TB_P.Text = TB_E.Text = "";
    }


    private void Btn_Encrypt_Click(object sender, RoutedEventArgs e)
    {
        PlainText = TB_PLAIN_TEXT.Text.Trim();
        var values = Encoding.ASCII.GetBytes(PlainText);

        foreach (var value in values)
        {
            MessageInIntegers.Add(value);
            EnChars.Add(Encrypt(value));
        }

        CipheredTextInInt = EnChars.Aggregate("", (current, c) => current + (c + " "));
        TB_CIPHERED_INT_TEXT.Text = CipheredTextInInt;

        CipheredTextInHex = string.Join("",
            values.Select(s => s.ToString("X4")));

        TB_CIPHERED_HEX_TEXT.Text = CipheredTextInHex;
        TB_CIPHERED_HEX_TEXT1.Text = CipheredTextInHex;
    }

    private int Encrypt(int messageChar)
    {
        var c = 1;
        for (var i = 0; i < E; i++)
            c = c * messageChar % N;
        c %= N;
        return c;
    }
    private char Decrypt(int messageChar)
    {
        var m = 1;
        for (var i = 0; i < D; i++)
            m = m * messageChar % N;
        m %= N;
        return (char)m;
    }

    private void BTN_CLEAR_Click_1(object sender, RoutedEventArgs e)
    {
        TB_CIPHERED_HEX_TEXT1.Text = TB_CIPHERED_IN_INTEGER.Text = TB_RESERSED_PLAIN_TEXT.Text = "";
    }

    private void BTN_EXIT_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void BTN_DECRYPT_Click(object sender, RoutedEventArgs e)
    {
        CipheredTextInHex = TB_CIPHERED_HEX_TEXT1.Text;
        var hexList = new List<string>();
        for (int i = 0; i < CipheredTextInHex.Length; i += 4)
        {
            var str = CipheredTextInHex.Substring(i, 4);
            hexList.Add(str);
        }

        //TB_CIPHERED_IN_INTEGER.Text = hexList.Aggregate("", (current, c) => current + (c + " "));
        foreach (var hexValue in hexList)
        {
            int intFromHex = int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
            MessageInIntegers.Add(intFromHex);
        }

        TB_CIPHERED_IN_INTEGER.Text = MessageInIntegers.Aggregate("", (current, c) => current + (c + " "));
    }
}