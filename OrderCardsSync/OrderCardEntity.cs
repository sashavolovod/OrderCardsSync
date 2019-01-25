using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class OrderCard
{
    public int CODE;
    public string KZNAME;
    public string OSNOVANIE;
    public DateTime DESIGNPLAN_DATE;
    public DateTime MAKEPLAN_DATE;
    public int QUANTITY;
    public string DSESIGN;
    public string DSENAME;
    public string OSNSIGN;
    public string OSNNAME;
    public string ZAKAZCIKSIGN;
    public string ZAKAZCIKNAME;
    public string IZGOTOVITELSIGN;
    public string IZGOTOVITELNAME;

    public override string ToString()
    {
        return CODE + ": " + IZGOTOVITELSIGN + " : " + KZNAME + " : " + OSNOVANIE + " : " + DESIGNPLAN_DATE.ToShortDateString();
    }
}
