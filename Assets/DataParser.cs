using UnityEngine;
using FileHelpers;
public class DataParser : MonoBehaviour
{
    public static float dollarBoxThicknessZ = 0.10922f; /* 100 paper bills */
    public static float dollarLengthX = 0.155956f;
    public static float dollarWidthY = 0.066294f;
    public int dollarZMultiplyer;
    public Transform physicalDollarAmount;

    [System.Serializable]
    [IgnoreFirst(1)]
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]

    public class graphBox
    {
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string AgencyCode;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string AgencyName;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string BureauCode;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string BureauName;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string AccountCode;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string AccountName;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string TreasuryAgencyCode;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string SubfunctionCode;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string SubfunctionTitle;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string BEAcatagory;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string GrantOrNonGrant;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string OnOrOffBudget;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1962;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1963;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1964;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1965;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1966;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1967;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1968;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1969;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1970;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1971;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1972;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1973;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1974;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1975;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1976;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year7677;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1977;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1978;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1979;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1980;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1981;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1982;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1983;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1984;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1985;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1986;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1987;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1988;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1989;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1990;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1991;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1992;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1993;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1994;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1995;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1996;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1997;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1998;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year1999;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year2000;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year2001;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year2002;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year2003;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year2004;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year2005;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year2006;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year2007;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year2008;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year2009;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year2010;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year2011;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year2012;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year2013;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year2014;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year2015;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year2016;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year2017;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year2018;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year2019;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public float year2020;

    }


    void Start()
    {
        physicalDollarAmount = GetComponent<Transform>();
        var engine = new FileHelperEngine<graphBox>();
        var result = engine.ReadFile("outlays.csv");
        int i = 1;
        foreach (graphBox box in result)
        {
            i++;
            Instantiate(physicalDollarAmount, new Vector3(i * 1, 1, 0), Quaternion.identity);

        }

    }


}
