using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public enum States
    {
        ready, drive, refueling, care, mustCare, mustRefuel
    }
    public enum Authorizations
    {
        User, PremiumUser, Admin, MainAdmin, IT
    }
    public enum Agency
    {
        רכבת_ישראל = 2, אגד, אגד_תעבורה, דן, ש_א_מ, נסיעות_ותיירות, גי_בי_טורס, מועצה_אזורית_אילות = 10,
        נתיב_אקספרס = 14, מטרופולין, סופרבוס, קווים = 18, כרמלית = 20, סיטיפס, גלים = 23, מועצה_אזורית_גולן,
        אפיקים, דן_צפון = 30, דן_בדרום, דן_באר_שבע, ירושלים_רמאללה_איחוד = 42, ירושלים_אבותור_ענתה_איחוד = 44,
        ירושלים_אלווסט_איחוד, ירושלים_הר_הזיתים = 47, ירושלים_עיסאוויה_איחוד = 49, ירושלים_דרום_איחוד,
        ירושלים_צורבאהר_איחוד, מוניות_מטרו_קו = 91, מוניות_שי_לי, מוניות_מאיה_יצחק_שדה, מוניות_שירן_נסיעות,
        מוניות_יהלום, מוניות_גלים, אודליה_מוניות, מוניות_רב_קווית, מוניות_הדר_לוד = 130
    }
    public enum TripType
    {
        LightTrain, Train = 2, Bus, Taxi = 8, FlexibleServiceLine = 715
    }
    public enum Areas
    {
        General, North, South, Center, Jerusalem, Lowland, JudeaAndSamaria
    }
}
