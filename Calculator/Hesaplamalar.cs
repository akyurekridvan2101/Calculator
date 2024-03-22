using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator
{
    class Hesaplamalar
    {
        // İşlem operatörlerini içeren bir dizi
        char[] operatorler = { '!', '^', '√', '×', '÷', '%', '+', '-' };

        // İfadeyi hesaplar ve sonucu döndürür
        public decimal IslemYap(string islem)
        {
            // İfadeyi parçalara ayırır
            List<string> parcalar = ParcalariAyir(islem, operatorler);

            // Parçalara ayrılmış ifade üzerinde işlemleri gerçekleştirir
            Hesapla(parcalar, '!', Faktoriyel);
            Hesapla(parcalar, '^', UsAlma);
            Hesapla(parcalar, '√', KokAlma);
            Hesapla(parcalar, '×', Carp);
            Hesapla(parcalar, '÷', Bol);
            Hesapla(parcalar, '%', ModAl);
            Hesapla(parcalar, '-', Cikar);
            Hesapla(parcalar, '+', Topla);

            // Hesaplanan sonucu döndürür
            return Convert.ToDecimal(parcalar.First());
        }


        // İfadeyi parçalara ayırır ve operatörler ile sayıları ayrı ayrı parçalara dizer
        private List<string> ParcalariAyir(string islem, char[] operatorler)
        {
            List<string> parcalar = new List<string>();
            string sayi = "";

            for (int i = 0; i < islem.Length; i++)
            {
                if (operatorler.Contains(islem[i]))
                {
                    if (sayi.Length != 0)
                        parcalar.Add(sayi); // Önceki sayıyı ekle
                    parcalar.Add(islem[i].ToString()); // Operatörü ekle
                    sayi = ""; // Sayıyı sıfırla
                }
                else if (char.IsDigit(islem[i]) || islem[i] == ',')
                {
                    sayi += islem[i]; // Sayıyı ekle
                }
                else
                {
                    if (sayi.Length != 0)
                    {
                        parcalar.Add(sayi); // Önceki sayıyı ekle
                        sayi = ""; // Sayıyı sıfırla
                    }

                    // Fonksiyonlara özel işlemleri kontrol et
                    if (islem[i..(i + 3)] == "sin" || islem[i..(i + 3)] == "cos" || islem[i..(i + 3)] == "tan" || islem[i..(i + 3)] == "cot" || islem[i..(i + 3)] == "log")
                    {
                        string trigonometrikFonksiyon = islem[i..(i + 3)];
                        int sonIndex = AralikBulma(islem, i + 4);
                        decimal sonuc = IslemYap(islem[(i + 4)..sonIndex]);

                        // İlgili trigonometrik fonksiyonu hesaplayıp sonucu ekler
                        switch (trigonometrikFonksiyon)
                        {
                            case "sin":
                                parcalar.Add(SinusAl(sonuc).ToString());
                                break;
                            case "cos":
                                parcalar.Add(CosinusAl(sonuc).ToString());
                                break;
                            case "tan":
                                parcalar.Add(TanjantAl(sonuc).ToString());
                                break;
                            case "cot":
                                parcalar.Add(CotanjantAl(sonuc).ToString());
                                break;
                        }

                        i = sonIndex; // İndeksi güncelle
                    }
                    else if (islem[i..(i + 2)] == "In")
                    {
                        int sonIndex = AralikBulma(islem, i + 3);
                        decimal inIci = IslemYap(islem[(i + 3)..sonIndex]);

                        // Euler logaritmasını alır ve sonucu ekler
                        parcalar.Add(LogaritmaAlEuler(inIci).ToString());

                        i = sonIndex; // İndeksi güncelle
                    }
                    else if (islem[i..(i + 3)] == "log")
                    {
                        int sonIndex = AralikBulma(islem, i + 4);
                        string[] logaritmaParcalari = islem[(i + 4)..sonIndex].Split(',');

                        decimal taban = IslemYap(logaritmaParcalari[0]);
                        decimal us = IslemYap(logaritmaParcalari[1]);

                        // Logaritmik işlemi yapar ve sonucu ekler
                        parcalar.Add(LogaritmaAl(taban, us).ToString());

                        i = sonIndex; // İndeksi güncelle
                    }
                    else if (islem[i..(i + 2)] == "C(")
                    {
                        int sonIndex = AralikBulma(islem, i + 3);
                        string[] kombinasyonParcalari = islem[(i + 3)..sonIndex].Split(',');

                        int n = Convert.ToInt32(kombinasyonParcalari[0]);
                        int r = Convert.ToInt32(kombinasyonParcalari[1]);

                        // Kombinasyon işlemi yapar ve sonucu ekler
                        parcalar.Add(KombinasyonAl(n, r).ToString());

                        i = sonIndex; // İndeksi güncelle
                    }
                    else if (islem[i..(i + 2)] == "P(")
                    {
                        int sonIndex = AralikBulma(islem, i + 3);
                        string[] permutasyonParcalari = islem[(i + 2)..sonIndex].Split(',');

                        int n = Convert.ToInt32(permutasyonParcalari[0]);
                        int r = Convert.ToInt32(permutasyonParcalari[1]);

                        // Permutasyon işlemi yapar ve sonucu ekler
                        parcalar.Add(PermutasyonAl(n, r).ToString());

                        i = sonIndex; // İndeksi güncelle
                    }
                }
            }

            if (sayi.Length != 0)
                parcalar.Add(sayi); // Kalan son sayıyı ekle
            return parcalar; // Parçalara ayrılmış ifadeyi döndür
        }


        // Belirli bir indeksten başlayarak, verilen ifade içindeki parantezin kapanışının indeksini bulur
        private int AralikBulma(string islem, int baslangicIndexi)
        {
            for (int i = baslangicIndexi; i < islem.Length; i++)
            {
                if (islem[i] == ')')
                    return i; // Parantez kapanışının indeksini döndürür

            }

            return islem.Length; // Eğer parantez kapatılmamışsa son indeksi döndürür
        }


        // İşlem operatörlerine göre parçaları ayırır
        private void Hesapla(List<string> parcalar, char hedefOperator, Func<decimal, decimal, decimal> Fonksiyon)
        {
            while (parcalar.Contains(hedefOperator.ToString()))
            {
                // Operatörün indeksini bul
                int index = parcalar.IndexOf(hedefOperator.ToString());

                decimal islemYapilacakSayi, sonuc, sonrakiSayi;
                if (hedefOperator == '√')
                {
                    // Kök işlemi için operandı al
                    islemYapilacakSayi = Convert.ToDecimal(parcalar[index + 1]);
                    // Kök alma fonksiyonunu çağır ve sonucu hesapla
                    sonuc = KokAlma(islemYapilacakSayi, 0);

                    // Sonucu parçalar listesine ekle ve işlemde kullanılan operatörü kaldır
                    parcalar[index + 1] = sonuc.ToString();
                    parcalar.RemoveAt(index);
                }
                else if (hedefOperator == '!')
                {
                    // Faktoriyel işlemi için operandı al
                    islemYapilacakSayi = Convert.ToDecimal(parcalar[index - 1]);
                    // Faktoriyel fonksiyonunu çağır ve sonucu hesapla
                    sonuc = Faktoriyel(islemYapilacakSayi, 0);

                    // Sonucu parçalar listesine ekle ve işlemde kullanılan operatörü kaldır
                    parcalar[index - 1] = sonuc.ToString();
                    parcalar.RemoveAt(index);
                }
                else
                {
                    // Diğer işlemler için operantları al
                    islemYapilacakSayi = Convert.ToDecimal(parcalar[index - 1]);
                    sonrakiSayi = Convert.ToDecimal(parcalar[index + 1]);
                    // İlgili fonksiyonu çağır ve sonucu hesapla
                    sonuc = Fonksiyon(islemYapilacakSayi, sonrakiSayi);

                    // Sonucu parçalar listesine ekle ve işlemde kullanılan operatörleri kaldır
                    parcalar[index - 1] = sonuc.ToString();
                    parcalar.RemoveAt(index);
                    parcalar.RemoveAt(index);
                }
            }
        }



        // İşlem fonksiyonları
        private decimal UsAlma(decimal a, decimal b) { return (decimal)Math.Pow((double)a, (double)b); }

        private decimal KokAlma(decimal a , decimal b) { return (decimal)Math.Sqrt((double)a); }// b parametresini sadece çift delegete uygun olması için ekledik kullanılmaz

        private decimal Bol(decimal a, decimal b) { return a / b; }

        private decimal Carp(decimal a, decimal b) { return a * b; }

        private decimal Topla(decimal a, decimal b) { return a + b; }

        private decimal Cikar(decimal a, decimal b) { return a - b; }

        private decimal ModAl(decimal a, decimal b) { return a % b; }

        private decimal Faktoriyel(decimal a , decimal b)// b parametresini sadece çift delegete uygun olması için ekledik kullanılmaz
        {
            if (a < 0)
                throw new InvalidOperationException("0'dan küçük sayıların faktöriyeli alınamaz");

            decimal faktoriyel = 1;// default olarak 1 verildiği için o'da for döngüsüne girmez ve 1 döner
            for (int i = 1; i <= a; i++)
                faktoriyel *= i;

            return faktoriyel;
        }

        private decimal SinusAl(decimal derece) { double radyan = (double)(derece / 180) * Math.PI; return (decimal)Math.Sin(radyan); }// D / 180 = R / PI mantığından yola çıktık.

        private decimal CosinusAl(decimal derece) { double radyan = (double)(derece / 180) * Math.PI; return (decimal)Math.Cos(radyan); }

        private decimal TanjantAl(decimal derece) { double radyan = (double)(derece / 180) * Math.PI; return (decimal)Math.Tan(radyan); }

        private decimal CotanjantAl(decimal derece) { return 1 / TanjantAl(derece); }

        private decimal LogaritmaAl(decimal taban , decimal us) { return (decimal)Math.Log((double)us, (double)taban); }// Logaritma taban tabanında us değerini alır.

        private decimal LogaritmaAlEuler(decimal sayi) { return (decimal)Math.Log((double)sayi, Math.E); }// Euler sayısı e tabanında logaritma alır.Yani doğal logaritma(In) alır.

        private decimal KombinasyonAl(int n, int r) { return Faktoriyel(n , 0) / (Faktoriyel(r, 0) * Faktoriyel(n - r , 0)); }

        private decimal PermutasyonAl(int n, int r) { return Faktoriyel(n , 0) / Faktoriyel(n - r , 0); }
    }
}
