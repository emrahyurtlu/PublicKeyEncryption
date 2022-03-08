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
            EnChars.Add(Encrypt(value));
        }


        var str = EnChars.Aggregate("", (current, c) => current + (c + " "));
        TB_CIPHERED_INT_TEXT.Text = str;
    }

    private int Encrypt(int messageChar)
    {
        return (int) (Math.Pow(messageChar, E) % N);
    }
}