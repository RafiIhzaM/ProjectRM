nomerDuaPuluhSatu();
Console.ReadKey();

static void nomerSatu()
{
        int[] kacamata = { 500, 600, 700, 800 };
        int[] baju = { 200, 400, 350 };
        int[] sepatu = { 400, 350, 200, 300 };
        int[] buku = { 100, 50, 150 };

        int budget = 1000;
        int bill = 0;
        int selPrev = 0;
        int selisihbudget = 0;


        List<string> belanja = new List<string>();

        for (int i = 0; i < kacamata.Length; i++)
        {
            for (int j = 0; j < baju.Length; j++)
            {
                for (int k = 0; k < sepatu.Length; k++)
                {
                    for (int l = 0; l < buku.Length; l++)
                    {
                        bill = kacamata[i] + baju[j] + sepatu[k] + buku[l];
                        selisihbudget = budget - bill;
                        if (bill <= 1000 && selisihbudget <= selPrev)
                        {
                            belanja.Clear();
                            belanja.Add($"kacamata {kacamata[i]}");
                            belanja.Add($"baju {baju[j]}");
                            belanja.Add($"sepatu {sepatu[k]}");
                            belanja.Add($"buku {buku[l]}");

                            selPrev = selisihbudget;
                        }


                    }
                }
            }
        }

        Console.WriteLine($"\nJumlah item : {belanja.Count()} ({String.Join(", ", belanja)})");

}

static void nomerDua()
{
    int a = 14;
    int b = 3;
    int c = 7;
    int d = 7;

    int denda = 100;
    int totalDenda = 0;

    int cek;

    Console.WriteLine("== Parsing Date Time ==");
    Console.Write("Masukan Peminjaman (dd/MM/yyyy) : ");
    string dateString = Console.ReadLine();
    Console.Write("Masukan Pengembalian (dd/MM/yyyy) : ");
    string dateString1 = Console.ReadLine();

    DateTime date1 = DateTime.ParseExact(dateString, "d/M/yyyy", null);
    DateTime date2 = DateTime.ParseExact(dateString1, "d/M/yyyy", null);
        //Console.WriteLine(date1);
    TimeSpan interval = date2 - date1;
    int inDays = interval.Days;


    cek = inDays - a;
    if (cek > 0)
    {
        totalDenda += cek * denda;
    }
    cek = inDays - b;
    if (cek > 0)
    {
        totalDenda += cek * denda;
    }
    cek = inDays - c;
    if (cek > 0)
    {
        totalDenda += cek * denda;
    }
    cek = inDays - d;
    if (cek > 0)
    {
        totalDenda += cek * denda;
    }



    Console.WriteLine("Jumlah Denda : " + totalDenda);
}

static void nomerTiga()
{
    double totalTarif = 0;
    Console.WriteLine("== Parsing Date Time ==");
    Console.Write("Waktu Masuk (dd/MM/yyyy|HH/mm/ss) : ");
    string dateString = Console.ReadLine();
    Console.Write("Waktu Keluar (dd/MM/yyyy) : ");
    string dateString1 = Console.ReadLine();

    DateTime date1 = DateTime.ParseExact(dateString, "d/M/yyyy|HH/mm/ss", null);
    DateTime date2 = DateTime.ParseExact(dateString1, "d/M/yyyy|H/m/s", null);
    //Console.WriteLine(date1);
    TimeSpan interval = date2 - date1;
    //Console.WriteLine("Lama Peminjaman : " + interval.Days);
    double inDays = interval.TotalSeconds;
    double jam = inDays / 3600;
    double hari = jam / 24;
    if (hari <= 1)
    {
        if (jam <= 8)
        {
            totalTarif += Math.Ceiling(jam) * 1000;
        }
        else
        {
            totalTarif = 8000;
        }
    }
    else
    {
        double sisaJam = Math.Ceiling(jam - 24) * 1000;
        totalTarif = 15000 + sisaJam;
    }

    Console.WriteLine("Jumlah Tarif : " + totalTarif);
}

static void nomerEmpat()
{
    int ulang, sisa, jumlahbilangan;
    int cek = 0;
    int tes = 1;
    List<int> list = new List<int>();
    Console.Write("masukan n = ");
    int bil = int.Parse(Console.ReadLine());
    for (int i = 0; i < bil;)
    {
        jumlahbilangan = 0;
        for (ulang = 1; ulang <= tes; ulang++)
        {
            sisa = tes % ulang;
            if (sisa == 0)
            {
                jumlahbilangan = jumlahbilangan + 1;
            }
            else
            {
                jumlahbilangan = jumlahbilangan;
            }
        }
        if (jumlahbilangan < 3)
        {
            // ket = "bukan bilangan prima";
            list.Add(tes);
            i++;


        }
        tes++;
    }
    Console.WriteLine(bil + " bilangan Prima Pertama ");
    foreach (int item in list)
    {
        Console.Write(item + " ");
    }
}

static void nomerLima()
{
    Console.Write("Masukkan N : ");
    int input = int.Parse(Console.ReadLine());
    int[] array = new int[input];
    // Console.Write("INput 1 : ");
    //array[0] = int.Parse(Console.ReadLine());
    array[0] = 0;
    array[1] = 1;

    for (int i = 0; i < array.Length; i++)
    {
        if (i >= 2)
        {
            array[i] = array[i - 1] + array[i - 2];
        }
        Console.Write(array[i]);

        if (i != array.Length - 1)
        {
            Console.Write(" ");
        }

    }
}



static void nomerEnam()
{
    Console.Write("Input : ");
    string text = Console.ReadLine();
    char[] cArray = text.ToCharArray();
    string reverse = String.Empty;
    for (int i = cArray.Length - 1; i > -1; i--)
    {
        reverse += cArray[i];
    }
    if (reverse == text)
    {
        Console.WriteLine("Polindrome");
    }
    else
    {
        Console.WriteLine("Tidak Polindrome");

    }
}

static void nomerTujuh()
{
    Console.Write("arr (ex = 5 6 7 0 1): ");
    string[] input = Console.ReadLine().Trim().Split(" ");
    int[] inputInt = Array.ConvertAll(input, int.Parse);
    Array.Sort(inputInt);
    //mencari rata rata
    float mean = 0;
    float count = 0;
    for (int a = 0; a < inputInt.Length; a++)
    {
        count += inputInt[a];
    }
    mean = count / inputInt.Length;


    //mencari median
    float median = 0;
    if (inputInt.Length % 2 == 0)
    {
        median = inputInt[(inputInt.Length / 2) - 1] + inputInt[inputInt.Length / 2];
        median = median / 2;
    }
    else
    {
        median = inputInt[(inputInt.Length / 2) - 1];
    }


    int max = 0;
    int modus = 0;
    for (int i = 0; i < inputInt.Length; i++)
    {
        int hitung = 0;
        for (int j = i; j < inputInt.Length; j++)
        {
            if (inputInt[j] == inputInt[i])
            {
                hitung++;
            }
        }
        if (hitung > max)
        {

            max = hitung;
            modus = inputInt[i];

        }

    }

    Console.WriteLine($"mean = {mean}");
    Console.WriteLine($"median = {median}");
    Console.WriteLine($"modus = {modus}");


}

static void nomerDelapan()
{
    List<int> array = new List<int>() { 1, 2, 4, 7, 8, 6, 9 };

    List<int> maxArray = array.GetRange(0, array.Count);
    int totalMax = 0;

    List<int> minArray = array.GetRange(0, array.Count);
    int totalMin = 0;

    for (int i = 0; i < 4; i++)
    {
        int max = 0;
        for (int j = 0; j < maxArray.Count; j++)
        {
            if (maxArray[j] > max)
            {
                max = maxArray[j];
            }
        }
        totalMax += max;
        maxArray.Remove(max);

        int min = int.MaxValue;
        for (int j = 0; j < minArray.Count; j++)
        {
            if (minArray[j] < min)
            {
                min = minArray[j];
            }
        }
        totalMin += min;
        minArray.Remove(min);
    }
    Console.WriteLine(totalMax);
    Console.WriteLine(totalMin);
}


static void nomerSembilan()
{
    Console.WriteLine("---DERET BILANGAN---");
    Console.Write("Masukan n : ");
    int n = int.Parse(Console.ReadLine());

    for (int i = 0; i < n; i++)
    {
        if (i == 0)
        {
            Console.Write($"{n} ");
        }
        else
        {
            Console.Write($"{n + (n * i)} ");
        }
    }
}

static void nomerSepuluh()
{
        Console.WriteLine("---SENSOR HURUF---");
        Console.Write("Masukan Kalimat : ");
        string kal = Console.ReadLine();
        string[] kalimat = kal.Split(" ");

        for (int i = 0; i < kalimat.Length; i++)
        {
            for (int j = 0; j < kalimat[i].Length; j++)
            {
                if (j != 0 && j != kalimat[i].Length - 1)
                {
                    Console.Write("*");
                }
                else
                {
                    Console.Write(kalimat[i][j]);
                }
            }
            Console.Write(" ");
        }
}

static void nomerSebelas()
{
        Console.WriteLine("Soal No 11");
        Console.Write("Masukkan Input : ");
        string input = Console.ReadLine().ToLower();
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input.Length; j++)
            {
                if (j == input.Length / 2)
                {
                    if ((input.Length / 2) % 2 == 0)
                    {
                        Console.Write($"{input[i]}");

                    }
                    else
                    {
                        Console.Write($"{input[i]}");
                        Console.Write("*");

                    }
                }
                else
                {
                    Console.Write("*");
                }
            }
            Console.WriteLine();
        }
}

static void nomerDuaBelas()
{
        Console.WriteLine("Soal No 6 (Sorting)");
        Console.Write("Input : ");
        string[] input = Console.ReadLine().Trim().Split(" ");
        int[] inputInt = Array.ConvertAll(input, int.Parse);
        // Array.Sort(input);

        int tampung = 0;
        for (int i = 0; i < inputInt.Length - 1; i++)
        {
            for (int j = i + 1; j < inputInt.Length; j++)
            {
                if (inputInt[j] < inputInt[i])
                {
                    tampung = inputInt[i];
                    inputInt[i] = inputInt[j];
                    inputInt[j] = tampung;
                }
                // tampung = Math.Min(inputInt[i], inputInt[j]);
            }
        }
        string output = string.Join((" "), inputInt);
        Console.WriteLine(output);
}

static void nomerTigaBelas()
{
    Console.Write("masukan Jam H:mm = ");
    string jam = Console.ReadLine();
    int[] array = Array.ConvertAll(jam.Split(':'), int.Parse);

    int x = array[0] * 30;
    int y = array[1] * 6;

    int hasil = Math.Abs(x - y);

    if (hasil > 180)
    {
        hasil = 360 - hasil;
    }
    Console.WriteLine("Jam " + jam + " -> " + hasil);



}

static void nomerEmpatBelas()
{
        Console.WriteLine("Soal Nomor 5");
        Console.Write("arr (ex = 5 6 7 0 1): ");
        string[] input = Console.ReadLine().Trim().Split(" ");
        Console.Write("rot : ");
        int rot = int.Parse(Console.ReadLine());

        Console.Write($"N = {rot} => ");
        for (int j = rot; j < input.Length; j++)
        {
            Console.Write(input[j] + " ");
        }
        for (int a = 0; a < rot; a++)
        {
            Console.Write(input[a]);
            if (a != rot - 1)
            {
                Console.Write(" ");
            }
        }

}


static void nomerLimaBelas()
{
        Console.WriteLine("Soal Nomor 15");
        Console.Write("Masukan Input Waktu (hh:mm:dd(PM/AM)) : ");
        string input = Console.ReadLine().Replace(" ", "").ToUpper();
        try
        {
            DateTime date1 = DateTime.ParseExact(input, "hh:mm:sstt", null);
            Console.WriteLine(date1.ToString("HH:mm:ss"));
        }
        catch (Exception e)
        {
            Console.WriteLine("Format yang dimasukkan salah");
            Console.WriteLine($"pesan eror : {e.Message}");
        }
}

static void nomerEnamBelas()
{
    List<string> list = new List<string>();
    List<char> ikanlist = new List<char>();
    List<double> dou = new List<double>();

cek:
    Console.Write("masukan Jenis Makanan = ");
    string kata = Console.ReadLine();
    list.Add(kata);

    Console.Write("Mengandung Ikan ? y/t = ");
    char ik = Convert.ToChar(Console.ReadLine());
    ikanlist.Add(ik);

    Console.Write("masukan harga = ");
    double harga = Convert.ToDouble(Console.ReadLine());
    dou.Add(harga);

    Console.Write("Tambah Makanan ? ya/tidak = ");
    string jawab = Console.ReadLine().ToLower();

    if (jawab == "ya")
    {
        goto cek;
    }

    double a = 0;
    double b = 0;

    for (int i = 0; i < list.Count; i++)
    {
        if (ikanlist[i] == 'y')
        {
            a += dou[i];
        }
        else
        {
            b += dou[i];
        }
    }

    double q = a * 10 / 100;
    double w = a * 5 / 100;
    double e = b * 10 / 100;
    double r = b * 5 / 100;

    double p = (a + b) * 10 / 100;
    double s = (a + b) * 5 / 100;
    double total = a + b;
    double bayar = a + b + p + s;

    double ptpt = (b + e + r) / 4;
    double pt = ptpt + ((a + q + w) / 3);

    for (int i = 0; i < list.Count; i++)
    {
        if (ikanlist[i] == 'y')
        {
            Console.WriteLine($"{list[i]} *ikan {dou[i]}");
        }
        else
        {
            Console.WriteLine($"{list[i]} {dou[i]}");
        }
    }
    Console.WriteLine("Total Harga Pesanan = " + total);
    Console.WriteLine("Pajak 10% = " + p);
    Console.WriteLine("Servis 5% = " + s);
    Console.WriteLine("Total Pembayaran = " + bayar);
    Console.WriteLine("Patungan 3 Orang yang tidak alergi = " + pt);
    Console.WriteLine("Patungan 1 Orang yang alergi = " + ptpt);

}

static void nomerTujuBelas()
{
    Console.WriteLine("---Naik Turun---");

    string hatori = "N N T N N N T T T T T N T T T N T N";
    string tampung = string.Join("", hatori.Split(' '));

    List<char> items = new List<char>(tampung);

    int satu = 0;
    int dua = 0;
    for (int i = 0; i < items.Count; i++)
    {
        if (items[i] == 'N')
        {
            satu++;
        }
        else if (items[i] == 'T')
        {
            satu--;
        }

        if (satu == 0)
        {
            dua++;
        }
    }

    Console.WriteLine(dua);
}

static void nomerDelapanBelas()
{
    List<double> kalori = new List<double>();
    List<double> jam = new List<double>();

    cek:

    Console.Write("Masukan Jam = ");
    double ik = Convert.ToDouble(Console.ReadLine());
    jam.Add(ik);

    Console.Write("Jumlah Kalori = ");
    double kal = Convert.ToDouble(Console.ReadLine());
    kalori.Add(kal);

    Console.Write("Tambah kue ? ya/tidak = ");
    string jawab = Console.ReadLine().ToLower();



    if (jawab == "ya")
    {
        goto cek;
    }
    Console.Write("Olahraga Jam = ");
    double ol = Convert.ToDouble(Console.ReadLine());

    Console.WriteLine("    Jam       Kalori");

    for (int i = 0; i < jam.Count; i++)
    {
        if (jam[i] < 10)
        {
            Console.WriteLine("      " + jam[i] + "        " + kalori[i]);
        }
        else
        {
            Console.WriteLine("     " + jam[i] + "        " + kalori[i]);
        }

    }

    double waktu = 0;
    double j, w;

    for (int i = 0; i < jam.Count; i++)
    {
        j = ol - jam[i];
        w = 0.1 * kalori[i] * (j * 60);
        waktu += w;
    }
    int nah = Convert.ToInt32(waktu);
    int minum = ((nah / 30) * 100) + 500;

    Console.WriteLine("Perlu Minum Sebanyak " + minum + " cc");
}



static void nomorSembilanBelas()
{
    Console.WriteLine("---PANGRAM---");

    string pangram = "abcdefghijklmnopqrstuvwxyz";

    Console.WriteLine("Masukan Kalimat = ");
    string kal = Console.ReadLine();

    int tampung = 0;
    for (int i = 0; i < kal.Length; i++)
    {
        for (int j = 0; j < pangram.Length; j++)
        {
            if (kal[i] == pangram[j])
            {
                tampung++;
                char a = pangram[j];
                pangram = pangram.Replace(a.ToString(), "");
            }
        }

        if (tampung == 26)
        {
            break;
        }
    }

    if (tampung == 26)
    {
        Console.WriteLine("Pangram");
    }
    else
    {
        Console.WriteLine("Not Pangram");
    }
}

static void nomerDuaPuluh()
{
    List<string> a = new List<string>();
    List<string> b = new List<string>();

    cek:

    Console.Write("Suit A = ");
    string sa = Console.ReadLine().ToLower();
    a.Add(sa);

    Console.Write("Suit B = ");
    string sb = Console.ReadLine().ToLower();
    b.Add(sb);

    Console.Write("Suit Lagi ? ya/tidak = ");
    string jawab = Console.ReadLine().ToLower();



    if (jawab == "ya")
    {
        goto cek;
    }

    Console.Write("Jarak Awal = ");
    int jarak = int.Parse(Console.ReadLine());

    int pa = 0; int pb = jarak;

    string win = "";

    for (int i = 0; i < a.Count; i++)
    {
        string ceka = a[i].ToLower();
        string cekb = b[i].ToLower();


        if (ceka == "g" && cekb == "b")
        {
            pb = pb + 1;
            win = "B";
        }
        if (ceka == "g" && cekb == "k")
        {
            pa = pa + 1;
            win = "A";
        }

        if (ceka == "b" && cekb == "k")
        {
            pb = pb + 1;
            win = "B";
        }
        if (ceka == "b" && cekb == "g")
        {
            pa = pa + 1;
            win = "A";
        }

        if (ceka == "k" && cekb == "b")
        {
            pa = pa + 1;
            win = "A";
        }
        if (ceka == "k" && cekb == "g")
        {
            pb = pb + 1;
            win = "B";
        }

        if (pa == pb)
        {
            Console.WriteLine("Yang Menang = " + win);
            break;
        }
    }

}


static void nomerDuaPuluhSatu()
{
    Console.WriteLine("---DISTANCE---");
    string pola = "_____O___finish";
    //Console.Write("Masukan Perjalanan : ");
    //string pola = Console.ReadLine();

    List<char> list = new List<char>();

    int goal = pola.Length - 6;
    int d = 0;
    int s = 0;
    
    for (int i = 0;i <= goal;i++)
    {
        if (pola[i + 1] == 'O' || pola[i] == 'O' && s >= 2)
        {
            d += 3;
            i += 2;
            s -= 2;
            list.Add('J');
        }
        else if(d + 2 == goal)
        {
            d += 2;
            i += 2;
            s -= 2;
            list.Add('J');
        }
        else if (pola[i] == 'O' && s < 2)
        {
            break;
        }
        else if (pola[i] == '_')
        {
            list.Add('W');
            d++;
            s++;
        }
    }

    if ( d == goal )
    {
        for (int i = 0; i < list.Count;i++)
        {
            Console.Write(list[i] + " ");
        }
    }
    else
    {
            Console.WriteLine("FAILED");
    }

}


static void nomerDuaPuluhDua()
{
    Console.WriteLine("nomer 22 tentang lilin yang meleleh");
    Console.Write("masukkan panjang lilin : ");
    string[] panjangLilin = Console.ReadLine().Trim().Split(" ");
    int[] panjangLilinInt = Array.ConvertAll(panjangLilin, int.Parse);
    int[] fibonacci = new int[panjangLilinInt.Length];
    fibonacci[0] = 0;
    fibonacci[1] = 1;

    for (int i = 0; i < fibonacci.Length; i++)
    {
        if (i >= 2)
        {
            fibonacci[i] = fibonacci[i - 1] + fibonacci[i - 2];
        }

    }

    bool end = true;
    do
    {
        for (int i = 0; i < panjangLilinInt.Length; i++)
        {
            panjangLilinInt[i] -= fibonacci[i];
            if (panjangLilinInt[i] <= 0)
            {
                end = false;
                Console.WriteLine($"Lilin yang habis duluan adalah lilin dengan index ke- {i}");
            }
        }
    } while (end);
}