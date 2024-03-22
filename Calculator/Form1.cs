using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Calculator
{
    public partial class HesapMakinesi : Form
    {

        Hesaplamalar sonucHesapla = new Hesaplamalar();

        List<char> operatorler = new List<char>() { '+', '-', '×', '÷', '%', '^', '√', '.' };// operatörlerin listesini oluşturduk. Bu listeyi kullanarak operatörlerin kontrolünü yapacağız.

        public HesapMakinesi()
        {
            InitializeComponent();
        }


        private void Sifir_Click(object sender, EventArgs e)
        {
            if (this.IslemEkrani.Text == "0")
                this.IslemEkrani.Text = "0";
            else
                this.IslemEkrani.Text += "0";
        }
        private void Bir_Click(object sender, EventArgs e)
        {
            if (this.IslemEkrani.Text == "0")
                this.IslemEkrani.Text = "1";
            else
                this.IslemEkrani.Text += "1";
        }
        private void Iki_Click(object sender, EventArgs e)
        {
            if (this.IslemEkrani.Text == "0")
                this.IslemEkrani.Text = "2";
            else
                this.IslemEkrani.Text += "2";
        }
        private void Uc_Click(object sender, EventArgs e)
        {
            if (this.IslemEkrani.Text == "0")
                this.IslemEkrani.Text = "3";
            else
                this.IslemEkrani.Text += "3";
        }
        private void Dort_Click(object sender, EventArgs e)
        {
            if (this.IslemEkrani.Text == "0")
                this.IslemEkrani.Text = "4";
            else
                this.IslemEkrani.Text += "4";
        }
        private void Bes_Click(object sender, EventArgs e)
        {
            if (this.IslemEkrani.Text == "0")
                this.IslemEkrani.Text = "5";
            else
                this.IslemEkrani.Text += "5";
        }
        private void Alti_Click(object sender, EventArgs e)
        {
            if (this.IslemEkrani.Text == "0")
                this.IslemEkrani.Text = "6";
            else
                this.IslemEkrani.Text += "6";
        }
        private void Yedi_Click(object sender, EventArgs e)
        {
            if (this.IslemEkrani.Text == "0")
                this.IslemEkrani.Text = "7";
            else
                this.IslemEkrani.Text += "7";
        }
        private void Sekiz_Click(object sender, EventArgs e)
        {
            if (this.IslemEkrani.Text == "0")
                this.IslemEkrani.Text = "8";
            else
                this.IslemEkrani.Text += "8";
        }
        private void Dokuz_Click(object sender, EventArgs e)
        {
            if (this.IslemEkrani.Text == "0")
                this.IslemEkrani.Text = "9";
            else
                this.IslemEkrani.Text += "9";
        }

        private void Faktoriyel_Click(object sender, EventArgs e)
        {
            this.IslemEkrani.Text += "!";
        }

        // Asal sayı butonuna tıklandığında çağrılacak olan fonksiyon
        private void AsalMi_Click(object sender, EventArgs e)
        {
            
            decimal sonuc = sonucHesapla.IslemYap(this.IslemEkrani.Text); // İşlemi yapar ve sonucu decimal olarak alırız

            if(sonuc % 1 != 0) // Eğer sonuç ondalıklı ise
            {
                this.SonucEkrani.Text = $"{sonuc} Asal değildir"; // Ekrana hata mesajı yazarız
                return; // return; kullanarak fonksiyonu sonlandırırız
            }

            bool asalMi = true; // Varsayılan olarak sayının asal olduğunu kabul ederiz

            // Sayının asal olup olmadığını kontrol ederiz
            if (sonuc == 2)
                asalMi = true;
            else if (sonuc < 2 || sonuc % 2 == 0)
                asalMi = false;
            else
                for (int i = 3; i <= Math.Sqrt((double)sonuc); i += 2)
                    // 2'den başlayarak sayının kareköküne kadar olan sayıları kontrol ederiz.
                    // Kareköküne kadar kontrol etmemizin nedeni, asal sayılarla ilgili verimli bir algoritma olmasıdır.
                    if (sonuc % i == 0)
                    {
                        asalMi = false;
                        break;
                    }

            // Sonucu ekrana yansıtırız
            if (asalMi)
                this.SonucEkrani.Text = $"{sonuc} Asaldır";
            else
                this.SonucEkrani.Text = $"{sonuc} Asal değildir";
        }


        private void Temizle_Click(object sender, EventArgs e)
        {
            this.IslemEkrani.Text = "0";// All Clear(AC) tuşuna basıldığında text "0" olarak sıfırlanır.
            this.SonucEkrani.Text = "";// All Clear(AC) tuşuna basıldığında sonuçlar da sıfırlanır
        }

        private void Permutasyon_Click(object sender, EventArgs e)
        {
            if (this.IslemEkrani.Text == "0")
                this.IslemEkrani.Text = "P(";
            else
                this.IslemEkrani.Text += "P(";
        }

        private void Kombinasyon_Click(object sender, EventArgs e)
        {
            if (this.IslemEkrani.Text == "0")
                this.IslemEkrani.Text = "C(";
            else
                this.IslemEkrani.Text += "C(";
        }

        private void EulerSayisi_Click(object sender, EventArgs e)
        {
            if (this.IslemEkrani.Text == "0")
                this.IslemEkrani.Text = "e";
            else
                this.IslemEkrani.Text += "e";
        }

        private void PiSayisi_Click(object sender, EventArgs e)
        {
            if (this.IslemEkrani.Text == "0")
                this.IslemEkrani.Text = "π";
            else
                this.IslemEkrani.Text += "π";
        }

        private void ParantezKapat_Click(object sender, EventArgs e)
        {
            if (this.IslemEkrani.Text == "0")
                this.IslemEkrani.Text = ")";
            else
                this.IslemEkrani.Text += ")";
        }

        private void ParantezAc_Click(object sender, EventArgs e)
        {
            if (this.IslemEkrani.Text == "0")
                this.IslemEkrani.Text = "(";
            else
                this.IslemEkrani.Text += "(";
        }

        private void Sinus_Click(object sender, EventArgs e)
        {
            if (this.IslemEkrani.Text == "0")
                this.IslemEkrani.Text = "sin(";
            else
                this.IslemEkrani.Text += "sin(";
        }

        private void Cosinus_Click(object sender, EventArgs e)
        {
            if (this.IslemEkrani.Text == "0")
                this.IslemEkrani.Text = "cos(";
            else
                this.IslemEkrani.Text += "cos(";
        }

        private void Tanjant_Click(object sender, EventArgs e)
        {
            if (this.IslemEkrani.Text == "0")
                this.IslemEkrani.Text = "tan(";
            else
                this.IslemEkrani.Text += "tan(";
        }

        private void Cotanjant_Click(object sender, EventArgs e)
        {
            if (this.IslemEkrani.Text == "0")
                this.IslemEkrani.Text = "cot(";
            else
                this.IslemEkrani.Text += "cot(";
        }

        private void Logaritma_Click(object sender, EventArgs e)
        {
            if (this.IslemEkrani.Text == "0")
                this.IslemEkrani.Text = "log(";
            else
                this.IslemEkrani.Text += "log(";
        }

        private void Elen_Click(object sender, EventArgs e)
        {
            if (this.IslemEkrani.Text == "0")
                this.IslemEkrani.Text = "In(";
            else
                this.IslemEkrani.Text += "In(";
        }

        private void Kok_Click(object sender, EventArgs e)
        {
            if (this.IslemEkrani.Text == "0")
                this.IslemEkrani.Text = "√";
            else
                this.IslemEkrani.Text += "√";
        }

        // Geri alma butonuna tıklandığında çağrılacak olan olay işleyicisi
        private void GeriAl_Click(object sender, EventArgs e)
        {
            if (IslemEkrani.Text.Length <= 1)
            {
                // İşlem ekranında sadece bir karakter kaldıysa ve bu karakter de 1 ise, ekranı sıfırla ve işlemi sonlandır.
                IslemEkrani.Text = "0";
                return;
            }

            // Geri alınacak öğelerin listesi
            string[] silinecekler = { "sin(", "cos(", "tan(", "cot(", "log(" };

            // Eğer işlem ekranının sonunda "In(" varsa, geri alma işlemi yapılır
            if (IslemEkrani.Text.EndsWith("In("))
            {
                if (IslemEkrani.Text.Length == 3)
                    IslemEkrani.Text = "0"; // Eğer sadece "In(" kısmı varsa, ekranı sıfırla.
                else
                    IslemEkrani.Text = IslemEkrani.Text[0..(IslemEkrani.Text.Length - 3)]; // "In(" kısmını sil.
                return;
            }

            // Silinecek öğelerin sonunda kontrol yapılır
            foreach (string silinecek in silinecekler)
            {
                if (IslemEkrani.Text.EndsWith(silinecek))
                {
                    if (IslemEkrani.Text.Length == 4)
                        IslemEkrani.Text = "0"; // Eğer sadece belirli bir fonksiyon varsa ve geri al tuşuna basıldıysa, ekranı sıfırla.
                    else
                        IslemEkrani.Text = IslemEkrani.Text[0..(IslemEkrani.Text.Length - 4)]; // Belirli bir fonksiyonun yanı sıra daha fazla içerik de varsa, sadece belirli fonksiyonu sil.
                    return;
                }
            }

            // Yukarıdaki koşullar sağlanmazsa, sadece son karakteri sil.
            IslemEkrani.Text = IslemEkrani.Text[0..^1];
        }


        // Bu fonksiyonlar operatörlerin kontrolünü yapar ve eğer operatörlerden biri varsa onun yerine yeni operatörü yazar.
        private void OperatorEkle(string op)
        {
            if (this.operatorler.Contains(this.IslemEkrani.Text[IslemEkrani.Text.Length - 1]))
            {
                // Eğer son karakter operatörler dizisinde varsa, o operatör yerine yeni operatörü yazarız.
                this.IslemEkrani.Text = this.IslemEkrani.Text[..^1] + op;
            }
            else if (this.IslemEkrani.Text == "0")
            {
                // Eğer işlem ekranı 0 ise, yeni operatörü ekleriz.
                this.IslemEkrani.Text = "0" + op;
            }
            else
            {
                // Hiçbir koşul sağlanmazsa, yeni operatörü işlem ekranına ekleriz.
                this.IslemEkrani.Text += op;
            }
        }


        private void Mod_Click(object sender, EventArgs e)
        {
            OperatorEkle("%");
        }

        private void Bolme_Click(object sender, EventArgs e)
        {
            OperatorEkle("÷");
        }

        private void Carpma_Click(object sender, EventArgs e)
        {
            OperatorEkle("×");
        }

        private void Cikartma_Click(object sender, EventArgs e)
        {
            OperatorEkle("-");
        }

        private void Toplama_Click(object sender, EventArgs e)
        {
            OperatorEkle("+");
        }

        private void Uslu_Click(object sender, EventArgs e)
        {
            OperatorEkle("^");
        }

        private void Esittir_Click(object sender, EventArgs e)
        {
            decimal sonuc = sonucHesapla.IslemYap(this.IslemEkrani.Text);

            if (sonuc % 1 == 0) // Çıktıda kesir kısmı yoksa
                this.SonucEkrani.Text = ((long)sonuc).ToString(); // Kesir kısmı yoksa sadece tam kısmı göster

            else // Çıktıda kesir kısmı varsa
            {
                string sonucumuz = sonuc.ToString(); // Decimal değeri stringe dönüştür

                int virgulIndex = sonucumuz.IndexOf(','); // Virgülün index'ini bul

                if (virgulIndex != -1 && sonucumuz.Length > virgulIndex + 5)
                {
                    string result = sonucumuz.Substring(virgulIndex + 1, 5); // Virgülden sonraki 5 değeri al
                    this.SonucEkrani.Text = sonucumuz[0..(virgulIndex + 1)] + sonucumuz[(virgulIndex + 1)..(virgulIndex + 6)];
                }
                else
                {
                    // Eğer virgül yoksa veya virgülden sonraki 5 karakter bulunmuyorsa, tüm sonucu göster
                    this.SonucEkrani.Text = sonucumuz;
                }
            }
        }

        private void Virgul_Click(object sender, EventArgs e)
        {
            if (char.IsDigit(this.IslemEkrani.Text[this.IslemEkrani.Text.Length - 1]))
            {
                OperatorEkle(",");
            }
            else
                return;
        }

    }
}
