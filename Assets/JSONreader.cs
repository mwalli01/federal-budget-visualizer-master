using UnityEngine;
using System.IO;
using LitJson;
using System.Data;
using System;
public class JSONreader : MonoBehaviour {
    private string JSONstring;
    private JsonData boxData;
    public DataTable table;
    public GameObject moneyBox;
    public GameObject EmptyBox;
    public GameObject MandatorySign;
    private float totaledMoney;
    private float totaledSubMoney;

    public float boxLength = 5f;
    public float boxHeight = 3f;
    public float boxWidth = 2f;

    private int distanceToMainBox;         // number used to keep account boxes close to the totals box after incrementing to the next agency
    private int distanceToSubBox;
    private float spacingTracker;
    private float subSpacingTracker;
    void Awake () {
        JSONstring = File.ReadAllText(Application.dataPath + "/FedOutlayArray.json");
        boxData = JsonMapper.ToObject(JSONstring);       
        JSONtoDataTable();
        FindRows();
        
    }

    public void ClearScene() {
       GameObject[] moneyboxes = GameObject.FindGameObjectsWithTag("moneybox");
        for(int i = 0; i < moneyboxes.Length; i++)
        {
            Destroy(moneyboxes[i]);
        }

    }

    

    public void FindRows() {
        //DataRow[] foundRows = table.Select(Query);
        //Debug.Log(foundRows.Length);
        //string selectTerm;
        //selectTerm = table.Rows[1][1].ToString();

        float goSideways = 10; // point on Z axis to align account boxes with their totals box
        float subGoSideways = 5; // point on X axis to align account boxes with their sub box
        for (int r = 1; r < table.Rows.Count; r++ )
        {
            DataRow row = table.Rows[r];
       
            string n = row.ItemArray[66].ToString();
            float p = float.Parse(n);

            if ((table.Rows[r][0].ToString() == (table.Rows[r - 1][0].ToString()) && r != 1)) // Add up the account balances for the total box
            {
                totaledMoney += p;
                distanceToMainBox++;

                if ((table.Rows[r][3].ToString() == table.Rows[r - 1][3].ToString()) && r != 1) // add account balances for sub box
                {
                    totaledSubMoney += p;
                    distanceToSubBox++;
                }
                else if ((table.Rows[r][3].ToString() != (table.Rows[r - 1][3].ToString()) && r != 1)) // spawn sub box
                {
                    if (totaledSubMoney != 0)                                                                        // conditional for empty/unused accounts
                    {
                        GameObject sb = (GameObject)Instantiate(moneyBox,
                                  (new Vector3((transform.position.x + subSpacingTracker),
                                                transform.position.y,
                                               (transform.position.z + goSideways))), transform.rotation);

                        BoxProperty subBox = sb.GetComponent<BoxProperty>();

                        if (totaledSubMoney < 0)
                        {
                            totaledSubMoney = totaledSubMoney * -1;
                            subBox.isNegative = true;
                        }

                        float subBoxtoCubeRoot = (totaledSubMoney / (boxLength * boxHeight * boxWidth));
                        float subBoxCubeRt = Mathf.Pow(subBoxtoCubeRoot, 1f / 3f);

                        subBox.dollarsTall = ((boxHeight * subBoxCubeRt) * 1);
                        subBox.dollarsWide = ((boxWidth * subBoxCubeRt) * 1);
                        subBox.dollarsLong = ((boxLength * subBoxCubeRt) * 1);

                        subSpacingTracker = ((boxLength * subBoxCubeRt) * 1) + 5;

                        float totalDisplayMoney = totaledSubMoney * 1000;

                        subBox.totalDollars = totalDisplayMoney.ToString("#,#");
                        subBox.BureauName = table.Rows[r - 1][3].ToString();
                        subBox.SizeChanger();

                        subBox.isNegative = false;
                        spacingTracker = 0;
                        subGoSideways += 15;
                        totaledSubMoney = 0;
                    }
                    else
                    {
                        subSpacingTracker += 5;
                        GameObject eb = (GameObject)Instantiate(EmptyBox,
                                        (new Vector3((transform.position.x + (subSpacingTracker)),
                                                      transform.position.y + 0.15f,
                                                     (transform.position.z + goSideways))), Quaternion.Euler(90, 0, 0));

                        BoxProperty emptyBox = eb.GetComponent<BoxProperty>();
                        emptyBox.isEmpty = true;
                        emptyBox.BureauName = table.Rows[r - 1][3].ToString();
                        emptyBox.SizeChanger();
                    }
                }
            }
            else if ((table.Rows[r][0].ToString() != (table.Rows[r - 1][0].ToString()) && r != 1)) // spawn the total box when iterator hits the next catagory
            {
                /* VVVV !!!REFACTOR THIS WHOLE DAMN BLOCK, JESUS CHIRST!!! VVVV */
                if (totaledMoney != 0)
                {
                    GameObject tb = (GameObject)Instantiate(moneyBox,
                              (new Vector3((transform.position.x - 15),
                                            transform.position.y,
                                            (transform.position.z + goSideways))), transform.rotation);

                    BoxProperty totalBox = tb.GetComponent<BoxProperty>();

                    if (totaledMoney < 0)
                    {
                        totaledMoney = totaledMoney * -1;
                        totalBox.isNegative = true;
                    }

                    float totalBoxtoCubeRoot = (totaledMoney / (boxLength * boxHeight * boxWidth));
                    float totalBoxcubeRt = Mathf.Pow(totalBoxtoCubeRoot, 1f / 3f);

                    totalBox.dollarsTall = ((boxHeight * totalBoxcubeRt) * 1);
                    totalBox.dollarsWide = ((boxWidth * totalBoxcubeRt) * 1);
                    totalBox.dollarsLong = ((boxLength * totalBoxcubeRt) * 1);

                    float totalDisplayMoney = totaledMoney * 1000;

                    totalBox.totalDollars = totalDisplayMoney.ToString("#,#");
                    totalBox.AgencyName = table.Rows[r - 1][1].ToString();
                    totalBox.SizeChanger();
                    goSideways += totalBox.dollarsWide; // spawns the next total box 20 units from the current one
                }
                else
                {
                    GameObject eb = (GameObject)Instantiate(EmptyBox,
                                                           (new Vector3((transform.position.x - 15),
                                                                         transform.position.y + 0.15f,
                                                                        (transform.position.z + goSideways))), Quaternion.Euler(90, 0, 0));
                    BoxProperty emptyBox = eb.GetComponent<BoxProperty>();
                    emptyBox.AgencyName = table.Rows[r - 1][1].ToString();
                    emptyBox.isEmpty = true;
                    emptyBox.SizeChanger();
                }

                
                totaledMoney = 0; // resets the value holding the totaled number
                

                subSpacingTracker = 0;
                subGoSideways = 0;
                distanceToMainBox = 0;
            }

            //GameObject mb = (GameObject)Instantiate(moneyBox, 
            //                (new Vector3(((transform.position.x + subGoSideways)), 
            //                            transform.position.y,
            //                            (transform.position.z - (spacingTracker / 2 )))), transform.rotation);
            
            //BoxProperty boxProperty = mb.GetComponent<BoxProperty>();
            
            
            //boxProperty.AgencyName = row.ItemArray[1].ToString();
            //boxProperty.BureauName = row.ItemArray[3].ToString();
            //boxProperty.AccountName = row.ItemArray[5].ToString();
            //boxProperty.SubfunctionTitle = row.ItemArray[8].ToString();
            //boxProperty.BEAcatagory = row.ItemArray[9].ToString();
            //boxProperty.GrantOrNonGrant = row.ItemArray[10].ToString();
            //boxProperty.OnOrOffBudget = row.ItemArray[11].ToString();

            

            //float dollarNormalizeStep1 = p * 1000;
            //string dollarNormalizeStep2 = dollarNormalizeStep1.ToString("#,#");
            //boxProperty.totalDollars = dollarNormalizeStep2;

            //if (p < 0)
            //{
            //    p = p * -1;
            //    boxProperty.isNegative = true;
            //}

            //float toCubeRoot = (p / (boxLength * boxHeight * boxWidth));
            //float cubeRt = Mathf.Pow(toCubeRoot, 1f / 3f);

            //boxProperty.dollarsTall = ((boxHeight * cubeRt) * 1);
            //boxProperty.dollarsWide = ((boxWidth * cubeRt) * 1);
            //boxProperty.dollarsLong = ((boxLength * cubeRt) * 1);

            //spacingTracker += ((boxLength * cubeRt) * 1);

            //boxProperty.SizeChanger();
            //boxProperty.isNegative = false;

           
        }
        
        //Debug.Log(table.Rows[int.Parse(Query)][int.Parse(Mod)]);
        //object result = table.Compute(Query, Mod);
        //Debug.Log(result.ToString());
        

    }



    /*list values 12 to 71(or end of the list) are years 1962 to 2020(or whatever the last year is).     
    List value 28 is the "1976 Transition Quarter" or "TQ", the year the Feds changed the beginning 
    of the fiscal year from July 1st to October 1st. 
    The budget data had no unique identifier for each account other than the lines they were on, hence the list..
    */


    public JsonData JSONtoDataTable()
    {
       
        for (int i = 0; i < boxData.Count; i++)                             // loop thru all lists in file
        {
            if (i > 0)                                                      // make sure i'm not looking at header
            {
                object[] array = new object[boxData[i].Count];              // create new array for data row

                for (int n = 0; n < boxData[i].Count; n++)                  // loop thru each item in row and add to array
                {
                    if (n >= 12)
                    {
                        string p = boxData[i][n].ToString();                // add rows as float if they are year columns
                        var k = float.Parse(p);
                        array[n] = k;                   
                    }
                    else
                    {
                        array[n] = boxData[i][n].ToString();               // else add as string
                    } 


                }

                table.Rows.Add(array);
            }

            if (i == 0)                             
            {                 
                for (int n = 0; n < boxData[0].Count; n++)
                {
                    object p = boxData[0][n];
                    var s = p.ToString();
                    if (n >= 12)
                    {
                        table.Columns.Add(s, typeof(float));            // add columns as float if they are year columns
                    }
                    else
                    {
                        table.Columns.Add(s, typeof(string));          // else add them as strings
                    }
                }
                
            }
            
        }
        return null;
    }
	
    
    
    
    // Update is called once per frame
	void Update () {
	
	}
}
