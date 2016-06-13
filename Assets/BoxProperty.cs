using UnityEngine;
using System.Collections;
using System.Data;
using System;
public class BoxProperty : MonoBehaviour {

    public float dollarsWide;
    public float dollarsTall;
    public float dollarsLong;

    public float manualBoxMoney;
    public string totalDollars;
    public float TotalDollars;
    public bool isNegative;
    public bool isEmpty;

    public string AgencyName;
    public string BureauName;
    public string AccountName;
    public string SubfunctionTitle;
    public string BEAcatagory;
    public string GrantOrNonGrant;
    public string OnOrOffBudget;

    public string query;
    private TextMesh dollarAmount;
    private float subGoSideways;

    void Start()
    {       
        //Mesh mesh = GetComponent<MeshFilter>().mesh;
        //Bounds bounds = mesh.bounds;
    }    

    void Update()
    {

    }

    public void QueryTable()
    {
        GameObject dataReader = GameObject.Find("DataReader");
        JSONreader jsonReader = dataReader.GetComponent<JSONreader>();
        DataRow[] foundRows = jsonReader.table.Select(query);
        Debug.Log(foundRows.Length);
        //string selectTerm;
        //selectTerm = jsonReader.table.Rows[1][1].ToString();
    }

    public void ManualBoxSpawn() // this spawns boxes using inputted values in the editor
    {
        GameObject dataReader = GameObject.Find("DataReader");
        JSONreader jsonReader = dataReader.GetComponent<JSONreader>();

        GameObject tb = (GameObject)Instantiate(jsonReader.moneyBox,
                            (new Vector3((transform.position.x ),
                                        transform.position.y,
                                        (transform.position.z))), transform.rotation);

        BoxProperty totalBox = tb.GetComponent<BoxProperty>();

        if (manualBoxMoney < 0)
        {
            manualBoxMoney = manualBoxMoney * -1;
            totalBox.isNegative = true;
        }

        float totalBoxtoCubeRoot = (manualBoxMoney / (jsonReader.boxLength * jsonReader.boxHeight * jsonReader.boxWidth));
        float totalBoxcubeRt = Mathf.Pow(totalBoxtoCubeRoot, 1f / 3f);

        totalBox.dollarsTall = ((jsonReader.boxHeight * totalBoxcubeRt) * 1);
        totalBox.dollarsWide = ((jsonReader.boxWidth * totalBoxcubeRt) * 1);
        totalBox.dollarsLong = ((jsonReader.boxLength * totalBoxcubeRt) * 1);

        float totalDisplayMoney = manualBoxMoney * 1000;

        totalBox.totalDollars = totalDisplayMoney.ToString("#,#");
        totalBox.AgencyName = AgencyName;
        totalBox.SizeChanger();
    }

    public void AccountBoxSpawner() // spawns the rows of account boxes starting from sub box position
    {
        GameObject dataReader = GameObject.Find("DataReader");
        JSONreader jsonReader = dataReader.GetComponent<JSONreader>();
        float spacingTracker = 0;
        float numberOfBoxes = 0;

        for (int r = 1; r < jsonReader.table.Rows.Count; r++)
        {
            DataRow row = jsonReader.table.Rows[r];

            string n = row.ItemArray[66].ToString();
            float p = float.Parse(n);


            if (row.ItemArray[3].ToString() == BureauName)
            {
                numberOfBoxes++;

                if (p != 0)  // Conditional for handling empty account boxes
                {
                    GameObject mb = (GameObject)Instantiate(jsonReader.moneyBox,
                                    (new Vector3(((transform.position.x + subGoSideways)),
                                                  (transform.position.y - transform.position.y),
                                                  (transform.position.z - spacingTracker))), 
                                                   transform.rotation);

                    BoxProperty boxProperty = mb.GetComponent<BoxProperty>();


                    boxProperty.AgencyName = row.ItemArray[1].ToString();
                    boxProperty.BureauName = row.ItemArray[3].ToString();
                    boxProperty.AccountName = row.ItemArray[5].ToString();
                    boxProperty.SubfunctionTitle = row.ItemArray[8].ToString();
                    boxProperty.BEAcatagory = row.ItemArray[9].ToString();
                    boxProperty.GrantOrNonGrant = row.ItemArray[10].ToString();
                    boxProperty.OnOrOffBudget = row.ItemArray[11].ToString();



                    float dollarNormalizeStep1 = p * 1000;
                    string dollarNormalizeStep2 = dollarNormalizeStep1.ToString("##,#");
                    boxProperty.totalDollars = dollarNormalizeStep2;

                    if (p < 0)
                    {
                        p = p * -1;
                        boxProperty.isNegative = true;
                    }

                    float toCubeRoot = (p / (jsonReader.boxLength * jsonReader.boxHeight * jsonReader.boxWidth));
                    float cubeRt = Mathf.Pow(toCubeRoot, 1f / 3f);

                    boxProperty.dollarsTall = ((jsonReader.boxHeight * cubeRt) * 1);
                    boxProperty.dollarsWide = ((jsonReader.boxWidth * cubeRt) * 1);
                    boxProperty.dollarsLong = ((jsonReader.boxLength * cubeRt) * 1);

                    spacingTracker += boxProperty.dollarsLong;

                    boxProperty.SizeChanger();
                    boxProperty.isNegative = false;
                }
                else                                                            // empty account spawner
                {
                    spacingTracker += 5;
                    GameObject eb = (GameObject)Instantiate(jsonReader.EmptyBox, 
                                                            new Vector3(transform.position.x + subGoSideways,
                                                                        transform.position.y + 0.15f,
                                                                        transform.position.z - (spacingTracker)), 
                                                                        Quaternion.Euler(90,0,0));
                    
                    BoxProperty emptyBox = eb.GetComponent<BoxProperty>();
                    emptyBox.AccountName = row.ItemArray[5].ToString();
                    emptyBox.SubfunctionTitle = row.ItemArray[8].ToString();
                    emptyBox.BEAcatagory = row.ItemArray[9].ToString();
                    emptyBox.GrantOrNonGrant = row.ItemArray[10].ToString();
                    emptyBox.OnOrOffBudget = row.ItemArray[11].ToString();
                    emptyBox.SizeChanger();
                }
            }
        }
    }


    public void SizeChanger()
    {
        TextMesh dollarAmount = GetComponentInChildren<TextMesh>();
        float dollarWideX = 0.155f * dollarsWide;
        float dollarTallY = 0.01f * dollarsTall;                                        // A stack of 10 bills
        float dollarLongZ = 0.066f * dollarsLong;
        TotalDollars = 1000 * (dollarsTall * (dollarsWide * dollarsLong));
        if (isEmpty != true)                                                            // conditional for empty accounts
        {            
            transform.localScale = new Vector3(dollarWideX, dollarTallY, dollarLongZ);  // define the box size
        }
        string display = AgencyName +                                                   // Set what to display on the textmesh
                         Environment.NewLine + 
                         BureauName + 
                         Environment.NewLine +
                         AccountName +
                         Environment.NewLine +
                         SubfunctionTitle +
                         Environment.NewLine +
                         BEAcatagory +
                         Environment.NewLine +
                         GrantOrNonGrant +
                         Environment.NewLine +
                         OnOrOffBudget +
                         Environment.NewLine +
                         totalDollars + " US Dollars"
                         ;
        dollarAmount.text = display;
        
        if (isNegative == true)
        {
            transform.localPosition = new Vector3((transform.position.x), (transform.position.y - (dollarTallY / 2)), (transform.position.z));
        }
        else
        {
            transform.localPosition = new Vector3((transform.position.x), (transform.position.y + (dollarTallY / 2)), (transform.position.z));
        }

        
        dollarAmount.anchor = TextAnchor.UpperLeft;
    }
    
    //public void MandatorySpawner()
    //{
    //    GameObject mSign = GameObject.Find()
    //    if (BEAcatagory == "Mandatory")
    //    {
    //        Instantiate()
    //    }
    //}
    /*public string AgencyCode;

    public string AgencyName;

    public string BureauCode;

    public string BureauName;

    public string AccountCode;

    public string AccountName;

    public string TreasuryAgencyCode;

    public string SubfunctionCode;

    public string SubfunctionTitle;

    public string BEAcatagory;

    public string GrantOrNonGrant;

    public string OnOrOffBudget;

    public float year1962;

    public float year1963;

    public float year1964;

    public float year1965;

    public float year1966;

    public float year1967;

    public float year1968;

    public float year1969;

    public float year1970;

    public float year1971;

    public float year1972;

    public float year1973;

    public float year1974;

    public float year1975;

    public float year1976;

    public float year7677;

    public float year1977;

    public float year1978;

    public float year1979;

    public float year1980;

    public float year1981;

    public float year1982;

    public float year1983;

    public float year1984;

    public float year1985;

    public float year1986;

    public float year1987;

    public float year1988;

    public float year1989;

    public float year1990;

    public float year1991;

    public float year1992;

    public float year1993;

    public float year1994;

    public float year1995;

    public float year1996;

    public float year1997;

    public float year1998;

    public float year1999;

    public float year2000;

    public float year2001;

    public float year2002;

    public float year2003;

    public float year2004;

    public float year2005;

    public float year2006;

    public float year2007;

    public float year2008;

    public float year2009;

    public float year2010;

    public float year2011;

    public float year2012;

    public float year2013;

    public float year2014;

    public float year2015;

    public float year2016;

    public float year2017;

    public float year2018;

    public float year2019;

    public float year2020;
*/


}
