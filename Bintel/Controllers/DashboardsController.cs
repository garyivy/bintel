using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Bintel.Models;
using Bintel.Models.AccountViewModels;
using Bintel.Services;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;

namespace Bintel.Controllers
{
    [Authorize]
    public class DashboardsController : Controller
    {

        private static DateTime CX(int SerialDate)
        {
            if (SerialDate > 59) SerialDate -= 1; //Excel/Lotus 2/29/1900 bug   
            return new DateTime(1899, 12, 31).AddDays(SerialDate);
        }
        private static DateTime CX2(int SerialDate)
        {
            if (SerialDate > 59) SerialDate -= 1; //Excel/Lotus 2/29/1900 bug   
            return new DateTime(1899, 12, 31).AddDays(SerialDate).AddYears(-1);
        }

        public class HighMetric
        {
            public DateTime Date { get; set; }
            public int High { get; set; }
        }

        public static List<HighMetric> highs = new List<HighMetric> {
new HighMetric { Date = CX2(42736), High = 65},
new HighMetric { Date = CX2(42737), High = 73},
new HighMetric { Date = CX2(42738), High = 61},
new HighMetric { Date = CX2(42739), High = 41},
new HighMetric { Date = CX2(42740), High = 41},
new HighMetric { Date = CX2(42741), High = 27},
new HighMetric { Date = CX2(42742), High = 39},
new HighMetric { Date = CX2(42743), High = 49},
new HighMetric { Date = CX2(42744), High = 68},
new HighMetric { Date = CX2(42745), High = 79},
new HighMetric { Date = CX2(42746), High = 79},
new HighMetric { Date = CX2(42747), High = 78},
new HighMetric { Date = CX2(42748), High = 52},
new HighMetric { Date = CX2(42749), High = 46},
new HighMetric { Date = CX2(42750), High = 67},
new HighMetric { Date = CX2(42751), High = 65},
new HighMetric { Date = CX2(42752), High = 52},
new HighMetric { Date = CX2(42753), High = 54},
new HighMetric { Date = CX2(42754), High = 65},
new HighMetric { Date = CX2(42755), High = 77},
new HighMetric { Date = CX2(42756), High = 72},
new HighMetric { Date = CX2(42757), High = 66},
new HighMetric { Date = CX2(42758), High = 68},
new HighMetric { Date = CX2(42759), High = 80},
new HighMetric { Date = CX2(42760), High = 66},
new HighMetric { Date = CX2(42761), High = 53},
new HighMetric { Date = CX2(42762), High = 57},
new HighMetric { Date = CX2(42763), High = 58},
new HighMetric { Date = CX2(42764), High = 68},
new HighMetric { Date = CX2(42765), High = 77},
new HighMetric { Date = CX2(42766), High = 80},
new HighMetric { Date = CX2(42767), High = 80},
new HighMetric { Date = CX2(42768), High = 55},
new HighMetric { Date = CX2(42769), High = 55},
new HighMetric { Date = CX2(42770), High = 53},
new HighMetric { Date = CX2(42771), High = 78},
new HighMetric { Date = CX2(42772), High = 80},
new HighMetric { Date = CX2(42773), High = 80},
new HighMetric { Date = CX2(42774), High = 69},
new HighMetric { Date = CX2(42775), High = 60},
new HighMetric { Date = CX2(42776), High = 76},
new HighMetric { Date = CX2(42777), High = 87},
new HighMetric { Date = CX2(42778), High = 74},
new HighMetric { Date = CX2(42779), High = 67},
new HighMetric { Date = CX2(42780), High = 49},
new HighMetric { Date = CX2(42781), High = 60},
new HighMetric { Date = CX2(42782), High = 68},
new HighMetric { Date = CX2(42783), High = 71},
new HighMetric { Date = CX2(42784), High = 81},
new HighMetric { Date = CX2(42785), High = 77},
new HighMetric { Date = CX2(42786), High = 71},
new HighMetric { Date = CX2(42787), High = 77},
new HighMetric { Date = CX2(42788), High = 84},
new HighMetric { Date = CX2(42789), High = 88},
new HighMetric { Date = CX2(42790), High = 72},
new HighMetric { Date = CX2(42791), High = 56},
new HighMetric { Date = CX2(42792), High = 63},
new HighMetric { Date = CX2(42793), High = 80},
new HighMetric { Date = CX2(42794), High = 82},
new HighMetric { Date = CX2(42795), High = 73},
new HighMetric { Date = CX2(42796), High = 67},
new HighMetric { Date = CX2(42797), High = 72},
new HighMetric { Date = CX2(42798), High = 60},
new HighMetric { Date = CX2(42799), High = 65},
new HighMetric { Date = CX2(42800), High = 82},
new HighMetric { Date = CX2(42801), High = 73},
new HighMetric { Date = CX2(42802), High = 78},
new HighMetric { Date = CX2(42803), High = 83},
new HighMetric { Date = CX2(42804), High = 75},
new HighMetric { Date = CX2(42805), High = 77},
new HighMetric { Date = CX2(42806), High = 55},
new HighMetric { Date = CX2(42807), High = 65},
new HighMetric { Date = CX2(42808), High = 65},
new HighMetric { Date = CX2(42809), High = 66},
new HighMetric { Date = CX2(42810), High = 74},
new HighMetric { Date = CX2(42811), High = 78},
new HighMetric { Date = CX2(42812), High = 84},
new HighMetric { Date = CX2(42813), High = 85},
new HighMetric { Date = CX2(42814), High = 90},
new HighMetric { Date = CX2(42815), High = 86},
new HighMetric { Date = CX2(42816), High = 84},
new HighMetric { Date = CX2(42817), High = 86},
new HighMetric { Date = CX2(42818), High = 79},
new HighMetric { Date = CX2(42819), High = 73},
new HighMetric { Date = CX2(42820), High = 85},
new HighMetric { Date = CX2(42821), High = 74},
new HighMetric { Date = CX2(42822), High = 83},
new HighMetric { Date = CX2(42823), High = 81},
new HighMetric { Date = CX2(42824), High = 79},
new HighMetric { Date = CX2(42825), High = 86},
new HighMetric { Date = CX2(42826), High = 79},
new HighMetric { Date = CX2(42827), High = 72},
new HighMetric { Date = CX2(42828), High = 80},
new HighMetric { Date = CX2(42829), High = 86},
new HighMetric { Date = CX2(42830), High = 73},
new HighMetric { Date = CX2(42831), High = 78},
new HighMetric { Date = CX2(42832), High = 80},
new HighMetric { Date = CX2(42833), High = 82},
new HighMetric { Date = CX2(42834), High = 84},
new HighMetric { Date = CX2(42835), High = 81},
new HighMetric { Date = CX2(42836), High = 76},
new HighMetric { Date = CX2(42837), High = 76},
new HighMetric { Date = CX2(42838), High = 82},
new HighMetric { Date = CX2(42839), High = 83},
new HighMetric { Date = CX2(42840), High = 81},
new HighMetric { Date = CX2(42841), High = 82},
new HighMetric { Date = CX2(42842), High = 77},
new HighMetric { Date = CX2(42843), High = 82},
new HighMetric { Date = CX2(42844), High = 85},
new HighMetric { Date = CX2(42845), High = 85},
new HighMetric { Date = CX2(42846), High = 86},
new HighMetric { Date = CX2(42847), High = 62},
new HighMetric { Date = CX2(42848), High = 75},
new HighMetric { Date = CX2(42849), High = 78},
new HighMetric { Date = CX2(42850), High = 86},
new HighMetric { Date = CX2(42851), High = 76},
new HighMetric { Date = CX2(42852), High = 76},
new HighMetric { Date = CX2(42853), High = 87},
new HighMetric { Date = CX2(42854), High = 90},
new HighMetric { Date = CX2(42855), High = 69},
new HighMetric { Date = CX2(42856), High = 84},
new HighMetric { Date = CX2(42857), High = 88},
new HighMetric { Date = CX2(42858), High = 78},
new HighMetric { Date = CX2(42859), High = 75},
new HighMetric { Date = CX2(42860), High = 81},
new HighMetric { Date = CX2(42861), High = 87},
new HighMetric { Date = CX2(42862), High = 87},
new HighMetric { Date = CX2(42863), High = 84},
new HighMetric { Date = CX2(42864), High = 82},
new HighMetric { Date = CX2(42865), High = 82},
new HighMetric { Date = CX2(42866), High = 87},
new HighMetric { Date = CX2(42867), High = 80},
new HighMetric { Date = CX2(42868), High = 85},
new HighMetric { Date = CX2(42869), High = 90},
new HighMetric { Date = CX2(42870), High = 89},
new HighMetric { Date = CX2(42871), High = 84},
new HighMetric { Date = CX2(42872), High = 91},
new HighMetric { Date = CX2(42873), High = 93},
new HighMetric { Date = CX2(42874), High = 89},
new HighMetric { Date = CX2(42875), High = 81},
new HighMetric { Date = CX2(42876), High = 81},
new HighMetric { Date = CX2(42877), High = 79},
new HighMetric { Date = CX2(42878), High = 77},
new HighMetric { Date = CX2(42879), High = 83},
new HighMetric { Date = CX2(42880), High = 92},
new HighMetric { Date = CX2(42881), High = 94},
new HighMetric { Date = CX2(42882), High = 91},
new HighMetric { Date = CX2(42883), High = 83},
new HighMetric { Date = CX2(42884), High = 88},
new HighMetric { Date = CX2(42885), High = 91},
new HighMetric { Date = CX2(42886), High = 92},
new HighMetric { Date = CX2(42887), High = 86},
new HighMetric { Date = CX2(42888), High = 84},
new HighMetric { Date = CX2(42889), High = 81},
new HighMetric { Date = CX2(42890), High = 89},
new HighMetric { Date = CX2(42891), High = 87},
new HighMetric { Date = CX2(42892), High = 94},
new HighMetric { Date = CX2(42893), High = 91},
new HighMetric { Date = CX2(42894), High = 88},
new HighMetric { Date = CX2(42895), High = 78},
new HighMetric { Date = CX2(42896), High = 89},
new HighMetric { Date = CX2(42897), High = 90},
new HighMetric { Date = CX2(42898), High = 93},
new HighMetric { Date = CX2(42899), High = 94},
new HighMetric { Date = CX2(42900), High = 94},
new HighMetric { Date = CX2(42901), High = 94},
new HighMetric { Date = CX2(42902), High = 96},
new HighMetric { Date = CX2(42903), High = 94},
new HighMetric { Date = CX2(42904), High = 94},
new HighMetric { Date = CX2(42905), High = 93},
new HighMetric { Date = CX2(42906), High = 93},
new HighMetric { Date = CX2(42907), High = 92},
new HighMetric { Date = CX2(42908), High = 90},
new HighMetric { Date = CX2(42909), High = 101},
new HighMetric { Date = CX2(42910), High = 84},
new HighMetric { Date = CX2(42911), High = 88},
new HighMetric { Date = CX2(42912), High = 88},
new HighMetric { Date = CX2(42913), High = 90},
new HighMetric { Date = CX2(42914), High = 90},
new HighMetric { Date = CX2(42915), High = 93},
new HighMetric { Date = CX2(42916), High = 96},
new HighMetric { Date = CX2(42917), High = 85},
new HighMetric { Date = CX2(42918), High = 95},
new HighMetric { Date = CX2(42919), High = 94},
new HighMetric { Date = CX2(42920), High = 93},
new HighMetric { Date = CX2(42921), High = 94},
new HighMetric { Date = CX2(42922), High = 96},
new HighMetric { Date = CX2(42923), High = 97},
new HighMetric { Date = CX2(42924), High = 98},
new HighMetric { Date = CX2(42925), High = 93},
new HighMetric { Date = CX2(42926), High = 94},
new HighMetric { Date = CX2(42927), High = 97},
new HighMetric { Date = CX2(42928), High = 95},
new HighMetric { Date = CX2(42929), High = 96},
new HighMetric { Date = CX2(42930), High = 100},
new HighMetric { Date = CX2(42931), High = 95},
new HighMetric { Date = CX2(42932), High = 98},
new HighMetric { Date = CX2(42933), High = 95},
new HighMetric { Date = CX2(42934), High = 97},
new HighMetric { Date = CX2(42935), High = 97},
new HighMetric { Date = CX2(42936), High = 99},
new HighMetric { Date = CX2(42937), High = 100},
new HighMetric { Date = CX2(42938), High = 100},
new HighMetric { Date = CX2(42939), High = 100},
new HighMetric { Date = CX2(42940), High = 91},
new HighMetric { Date = CX2(42941), High = 97},
new HighMetric { Date = CX2(42942), High = 98},
new HighMetric { Date = CX2(42943), High = 101},
new HighMetric { Date = CX2(42944), High = 104},
new HighMetric { Date = CX2(42945), High = 98},
new HighMetric { Date = CX2(42946), High = 94},
new HighMetric { Date = CX2(42947), High = 92},
new HighMetric { Date = CX2(42948), High = 93},
new HighMetric { Date = CX2(42949), High = 84},
new HighMetric { Date = CX2(42950), High = 93},
new HighMetric { Date = CX2(42951), High = 97},
new HighMetric { Date = CX2(42952), High = 97},
new HighMetric { Date = CX2(42953), High = 99},
new HighMetric { Date = CX2(42954), High = 94},
new HighMetric { Date = CX2(42955), High = 90},
new HighMetric { Date = CX2(42956), High = 91},
new HighMetric { Date = CX2(42957), High = 99},
new HighMetric { Date = CX2(42958), High = 101},
new HighMetric { Date = CX2(42959), High = 97},
new HighMetric { Date = CX2(42960), High = 91},
new HighMetric { Date = CX2(42961), High = 92},
new HighMetric { Date = CX2(42962), High = 96},
new HighMetric { Date = CX2(42963), High = 96},
new HighMetric { Date = CX2(42964), High = 92},
new HighMetric { Date = CX2(42965), High = 94},
new HighMetric { Date = CX2(42966), High = 99},
new HighMetric { Date = CX2(42967), High = 98},
new HighMetric { Date = CX2(42968), High = 96},
new HighMetric { Date = CX2(42969), High = 98},
new HighMetric { Date = CX2(42970), High = 91},
new HighMetric { Date = CX2(42971), High = 90},
new HighMetric { Date = CX2(42972), High = 88},
new HighMetric { Date = CX2(42973), High = 83},
new HighMetric { Date = CX2(42974), High = 83},
new HighMetric { Date = CX2(42975), High = 88},
new HighMetric { Date = CX2(42976), High = 90},
new HighMetric { Date = CX2(42977), High = 89},
new HighMetric { Date = CX2(42978), High = 90},
new HighMetric { Date = CX2(42979), High = 92},
new HighMetric { Date = CX2(42980), High = 94},
new HighMetric { Date = CX2(42981), High = 95},
new HighMetric { Date = CX2(42982), High = 95},
new HighMetric { Date = CX2(42983), High = 93},
new HighMetric { Date = CX2(42984), High = 85},
new HighMetric { Date = CX2(42985), High = 86},
new HighMetric { Date = CX2(42986), High = 85},
new HighMetric { Date = CX2(42987), High = 87},
new HighMetric { Date = CX2(42988), High = 86},
new HighMetric { Date = CX2(42989), High = 87},
new HighMetric { Date = CX2(42990), High = 85},
new HighMetric { Date = CX2(42991), High = 91},
new HighMetric { Date = CX2(42992), High = 98},
new HighMetric { Date = CX2(42993), High = 94},
new HighMetric { Date = CX2(42994), High = 95},
new HighMetric { Date = CX2(42995), High = 95},
new HighMetric { Date = CX2(42996), High = 94},
new HighMetric { Date = CX2(42997), High = 97},
new HighMetric { Date = CX2(42998), High = 98},
new HighMetric { Date = CX2(42999), High = 97},
new HighMetric { Date = CX2(43000), High = 97},
new HighMetric { Date = CX2(43001), High = 95},
new HighMetric { Date = CX2(43002), High = 93},
new HighMetric { Date = CX2(43003), High = 93},
new HighMetric { Date = CX2(43004), High = 91},
new HighMetric { Date = CX2(43005), High = 92},
new HighMetric { Date = CX2(43006), High = 81},
new HighMetric { Date = CX2(43007), High = 73},
new HighMetric { Date = CX2(43008), High = 86},
new HighMetric { Date = CX2(43009), High = 89},
new HighMetric { Date = CX2(43010), High = 91},
new HighMetric { Date = CX2(43011), High = 88},
new HighMetric { Date = CX2(43012), High = 88},
new HighMetric { Date = CX2(43013), High = 88},
new HighMetric { Date = CX2(43014), High = 89},
new HighMetric { Date = CX2(43015), High = 89},
new HighMetric { Date = CX2(43016), High = 91},
new HighMetric { Date = CX2(43017), High = 94},
new HighMetric { Date = CX2(43018), High = 72},
new HighMetric { Date = CX2(43019), High = 77},
new HighMetric { Date = CX2(43020), High = 87},
new HighMetric { Date = CX2(43021), High = 92},
new HighMetric { Date = CX2(43022), High = 93},
new HighMetric { Date = CX2(43023), High = 81},
new HighMetric { Date = CX2(43024), High = 75},
new HighMetric { Date = CX2(43025), High = 76},
new HighMetric { Date = CX2(43026), High = 83},
new HighMetric { Date = CX2(43027), High = 82},
new HighMetric { Date = CX2(43028), High = 84},
new HighMetric { Date = CX2(43029), High = 89},
new HighMetric { Date = CX2(43030), High = 75},
new HighMetric { Date = CX2(43031), High = 83},
new HighMetric { Date = CX2(43032), High = 74},
new HighMetric { Date = CX2(43033), High = 80},
new HighMetric { Date = CX2(43034), High = 85},
new HighMetric { Date = CX2(43035), High = 67},
new HighMetric { Date = CX2(43036), High = 60},
new HighMetric { Date = CX2(43037), High = 78},
new HighMetric { Date = CX2(43038), High = 77},
new HighMetric { Date = CX2(43039), High = 61},
new HighMetric { Date = CX2(43040), High = 80},
new HighMetric { Date = CX2(43041), High = 92},
new HighMetric { Date = CX2(43042), High = 74},
new HighMetric { Date = CX2(43043), High = 88},
new HighMetric { Date = CX2(43044), High = 92},
new HighMetric { Date = CX2(43045), High = 75},
new HighMetric { Date = CX2(43046), High = 62},
new HighMetric { Date = CX2(43047), High = 50},
new HighMetric { Date = CX2(43048), High = 54},
new HighMetric { Date = CX2(43049), High = 64},
new HighMetric { Date = CX2(43050), High = 70},
new HighMetric { Date = CX2(43051), High = 71},
new HighMetric { Date = CX2(43052), High = 67},
new HighMetric { Date = CX2(43053), High = 79},
new HighMetric { Date = CX2(43054), High = 74},
new HighMetric { Date = CX2(43055), High = 75},
new HighMetric { Date = CX2(43056), High = 82},
new HighMetric { Date = CX2(43057), High = 76},
new HighMetric { Date = CX2(43058), High = 63},
new HighMetric { Date = CX2(43059), High = 66},
new HighMetric { Date = CX2(43060), High = 70},
new HighMetric { Date = CX2(43061), High = 56},
new HighMetric { Date = CX2(43062), High = 68},
new HighMetric { Date = CX2(43063), High = 78},
new HighMetric { Date = CX2(43064), High = 75},
new HighMetric { Date = CX2(43065), High = 73},
new HighMetric { Date = CX2(43066), High = 76},
new HighMetric { Date = CX2(43067), High = 78},
new HighMetric { Date = CX2(43068), High = 66},
new HighMetric { Date = CX2(43069), High = 76},
new HighMetric { Date = CX2(43070), High = 73},
new HighMetric { Date = CX2(43071), High = 78},
new HighMetric { Date = CX2(43072), High = 77},
new HighMetric { Date = CX2(43073), High = 82},
new HighMetric { Date = CX2(43074), High = 58},
new HighMetric { Date = CX2(43075), High = 55},
new HighMetric { Date = CX2(43076), High = 51},
new HighMetric { Date = CX2(43077), High = 48},
new HighMetric { Date = CX2(43078), High = 60},
new HighMetric { Date = CX2(43079), High = 72},
new HighMetric { Date = CX2(43080), High = 77},
new HighMetric { Date = CX2(43081), High = 57},
new HighMetric { Date = CX2(43082), High = 75},
new HighMetric { Date = CX2(43083), High = 62},
new HighMetric { Date = CX2(43084), High = 56},
new HighMetric { Date = CX2(43085), High = 63},
new HighMetric { Date = CX2(43086), High = 52},
new HighMetric { Date = CX2(43087), High = 53},
new HighMetric { Date = CX2(43088), High = 61},
new HighMetric { Date = CX2(43089), High = 61},
new HighMetric { Date = CX2(43090), High = 77},
new HighMetric { Date = CX2(43091), High = 68},
new HighMetric { Date = CX2(43092), High = 51},
new HighMetric { Date = CX2(43093), High = 51},
new HighMetric { Date = CX2(43094), High = 51},
new HighMetric { Date = CX2(43095), High = 32},
new HighMetric { Date = CX2(43096), High = 25},
new HighMetric { Date = CX2(43097), High = 32},
new HighMetric { Date = CX2(43098), High = 33},
new HighMetric { Date = CX2(43099), High = 34},
new HighMetric { Date = CX2(43100), High = 35},

new HighMetric { Date = CX(42736), High = 65},
new HighMetric { Date = CX(42737), High = 73},
new HighMetric { Date = CX(42738), High = 61},
new HighMetric { Date = CX(42739), High = 41},
new HighMetric { Date = CX(42740), High = 41},
new HighMetric { Date = CX(42741), High = 27},
new HighMetric { Date = CX(42742), High = 39},
new HighMetric { Date = CX(42743), High = 49},
new HighMetric { Date = CX(42744), High = 68},
new HighMetric { Date = CX(42745), High = 79},
new HighMetric { Date = CX(42746), High = 79},
new HighMetric { Date = CX(42747), High = 78},
new HighMetric { Date = CX(42748), High = 52},
new HighMetric { Date = CX(42749), High = 46},
new HighMetric { Date = CX(42750), High = 67},
new HighMetric { Date = CX(42751), High = 65},
new HighMetric { Date = CX(42752), High = 52},
new HighMetric { Date = CX(42753), High = 54},
new HighMetric { Date = CX(42754), High = 65},
new HighMetric { Date = CX(42755), High = 77},
new HighMetric { Date = CX(42756), High = 72},
new HighMetric { Date = CX(42757), High = 66},
new HighMetric { Date = CX(42758), High = 68},
new HighMetric { Date = CX(42759), High = 80},
new HighMetric { Date = CX(42760), High = 66},
new HighMetric { Date = CX(42761), High = 53},
new HighMetric { Date = CX(42762), High = 57},
new HighMetric { Date = CX(42763), High = 58},
new HighMetric { Date = CX(42764), High = 68},
new HighMetric { Date = CX(42765), High = 77},
new HighMetric { Date = CX(42766), High = 80},
new HighMetric { Date = CX(42767), High = 80},
new HighMetric { Date = CX(42768), High = 55},
new HighMetric { Date = CX(42769), High = 55},
new HighMetric { Date = CX(42770), High = 53},
new HighMetric { Date = CX(42771), High = 78},
new HighMetric { Date = CX(42772), High = 80},
new HighMetric { Date = CX(42773), High = 80},
new HighMetric { Date = CX(42774), High = 69},
new HighMetric { Date = CX(42775), High = 60},
new HighMetric { Date = CX(42776), High = 76},
new HighMetric { Date = CX(42777), High = 87},
new HighMetric { Date = CX(42778), High = 74},
new HighMetric { Date = CX(42779), High = 67},
new HighMetric { Date = CX(42780), High = 49},
new HighMetric { Date = CX(42781), High = 60},
new HighMetric { Date = CX(42782), High = 68},
new HighMetric { Date = CX(42783), High = 71},
new HighMetric { Date = CX(42784), High = 81},
new HighMetric { Date = CX(42785), High = 77},
new HighMetric { Date = CX(42786), High = 71},
new HighMetric { Date = CX(42787), High = 77},
new HighMetric { Date = CX(42788), High = 84},
new HighMetric { Date = CX(42789), High = 88},
new HighMetric { Date = CX(42790), High = 72},
new HighMetric { Date = CX(42791), High = 56},
new HighMetric { Date = CX(42792), High = 63},
new HighMetric { Date = CX(42793), High = 80},
new HighMetric { Date = CX(42794), High = 82},
new HighMetric { Date = CX(42795), High = 73},
new HighMetric { Date = CX(42796), High = 67},
new HighMetric { Date = CX(42797), High = 72},
new HighMetric { Date = CX(42798), High = 60},
new HighMetric { Date = CX(42799), High = 65},
new HighMetric { Date = CX(42800), High = 82},
new HighMetric { Date = CX(42801), High = 73},
new HighMetric { Date = CX(42802), High = 78},
new HighMetric { Date = CX(42803), High = 83},
new HighMetric { Date = CX(42804), High = 75},
new HighMetric { Date = CX(42805), High = 77},
new HighMetric { Date = CX(42806), High = 55},
new HighMetric { Date = CX(42807), High = 65},
new HighMetric { Date = CX(42808), High = 65},
new HighMetric { Date = CX(42809), High = 66},
new HighMetric { Date = CX(42810), High = 74},
new HighMetric { Date = CX(42811), High = 78},
new HighMetric { Date = CX(42812), High = 84},
new HighMetric { Date = CX(42813), High = 85},
new HighMetric { Date = CX(42814), High = 90},
new HighMetric { Date = CX(42815), High = 86},
new HighMetric { Date = CX(42816), High = 84},
new HighMetric { Date = CX(42817), High = 86},
new HighMetric { Date = CX(42818), High = 79},
new HighMetric { Date = CX(42819), High = 73},
new HighMetric { Date = CX(42820), High = 85},
new HighMetric { Date = CX(42821), High = 74},
new HighMetric { Date = CX(42822), High = 83},
new HighMetric { Date = CX(42823), High = 81},
new HighMetric { Date = CX(42824), High = 79},
new HighMetric { Date = CX(42825), High = 86},
new HighMetric { Date = CX(42826), High = 79},
new HighMetric { Date = CX(42827), High = 72},
new HighMetric { Date = CX(42828), High = 80},
new HighMetric { Date = CX(42829), High = 86},
new HighMetric { Date = CX(42830), High = 73},
new HighMetric { Date = CX(42831), High = 78},
new HighMetric { Date = CX(42832), High = 80},
new HighMetric { Date = CX(42833), High = 82},
new HighMetric { Date = CX(42834), High = 84},
new HighMetric { Date = CX(42835), High = 81},
new HighMetric { Date = CX(42836), High = 76},
new HighMetric { Date = CX(42837), High = 76},
new HighMetric { Date = CX(42838), High = 82},
new HighMetric { Date = CX(42839), High = 83},
new HighMetric { Date = CX(42840), High = 81},
new HighMetric { Date = CX(42841), High = 82},
new HighMetric { Date = CX(42842), High = 77},
new HighMetric { Date = CX(42843), High = 82},
new HighMetric { Date = CX(42844), High = 85},
new HighMetric { Date = CX(42845), High = 85},
new HighMetric { Date = CX(42846), High = 86},
new HighMetric { Date = CX(42847), High = 62},
new HighMetric { Date = CX(42848), High = 75},
new HighMetric { Date = CX(42849), High = 78},
new HighMetric { Date = CX(42850), High = 86},
new HighMetric { Date = CX(42851), High = 76},
new HighMetric { Date = CX(42852), High = 76},
new HighMetric { Date = CX(42853), High = 87},
new HighMetric { Date = CX(42854), High = 90},
new HighMetric { Date = CX(42855), High = 69},
new HighMetric { Date = CX(42856), High = 84},
new HighMetric { Date = CX(42857), High = 88},
new HighMetric { Date = CX(42858), High = 78},
new HighMetric { Date = CX(42859), High = 75},
new HighMetric { Date = CX(42860), High = 81},
new HighMetric { Date = CX(42861), High = 87},
new HighMetric { Date = CX(42862), High = 87},
new HighMetric { Date = CX(42863), High = 84},
new HighMetric { Date = CX(42864), High = 82},
new HighMetric { Date = CX(42865), High = 82},
new HighMetric { Date = CX(42866), High = 87},
new HighMetric { Date = CX(42867), High = 80},
new HighMetric { Date = CX(42868), High = 85},
new HighMetric { Date = CX(42869), High = 90},
new HighMetric { Date = CX(42870), High = 89},
new HighMetric { Date = CX(42871), High = 84},
new HighMetric { Date = CX(42872), High = 91},
new HighMetric { Date = CX(42873), High = 93},
new HighMetric { Date = CX(42874), High = 89},
new HighMetric { Date = CX(42875), High = 81},
new HighMetric { Date = CX(42876), High = 81},
new HighMetric { Date = CX(42877), High = 79},
new HighMetric { Date = CX(42878), High = 77},
new HighMetric { Date = CX(42879), High = 83},
new HighMetric { Date = CX(42880), High = 92},
new HighMetric { Date = CX(42881), High = 94},
new HighMetric { Date = CX(42882), High = 91},
new HighMetric { Date = CX(42883), High = 83},
new HighMetric { Date = CX(42884), High = 88},
new HighMetric { Date = CX(42885), High = 91},
new HighMetric { Date = CX(42886), High = 92},
new HighMetric { Date = CX(42887), High = 86},
new HighMetric { Date = CX(42888), High = 84},
new HighMetric { Date = CX(42889), High = 81},
new HighMetric { Date = CX(42890), High = 89},
new HighMetric { Date = CX(42891), High = 87},
new HighMetric { Date = CX(42892), High = 94},
new HighMetric { Date = CX(42893), High = 91},
new HighMetric { Date = CX(42894), High = 88},
new HighMetric { Date = CX(42895), High = 78},
new HighMetric { Date = CX(42896), High = 89},
new HighMetric { Date = CX(42897), High = 90},
new HighMetric { Date = CX(42898), High = 93},
new HighMetric { Date = CX(42899), High = 94},
new HighMetric { Date = CX(42900), High = 94},
new HighMetric { Date = CX(42901), High = 94},
new HighMetric { Date = CX(42902), High = 96},
new HighMetric { Date = CX(42903), High = 94},
new HighMetric { Date = CX(42904), High = 94},
new HighMetric { Date = CX(42905), High = 93},
new HighMetric { Date = CX(42906), High = 93},
new HighMetric { Date = CX(42907), High = 92},
new HighMetric { Date = CX(42908), High = 90},
new HighMetric { Date = CX(42909), High = 101},
new HighMetric { Date = CX(42910), High = 84},
new HighMetric { Date = CX(42911), High = 88},
new HighMetric { Date = CX(42912), High = 88},
new HighMetric { Date = CX(42913), High = 90},
new HighMetric { Date = CX(42914), High = 90},
new HighMetric { Date = CX(42915), High = 93},
new HighMetric { Date = CX(42916), High = 96},
new HighMetric { Date = CX(42917), High = 85},
new HighMetric { Date = CX(42918), High = 95},
new HighMetric { Date = CX(42919), High = 94},
new HighMetric { Date = CX(42920), High = 93},
new HighMetric { Date = CX(42921), High = 94},
new HighMetric { Date = CX(42922), High = 96},
new HighMetric { Date = CX(42923), High = 97},
new HighMetric { Date = CX(42924), High = 98},
new HighMetric { Date = CX(42925), High = 93},
new HighMetric { Date = CX(42926), High = 94},
new HighMetric { Date = CX(42927), High = 97},
new HighMetric { Date = CX(42928), High = 95},
new HighMetric { Date = CX(42929), High = 96},
new HighMetric { Date = CX(42930), High = 100},
new HighMetric { Date = CX(42931), High = 95},
new HighMetric { Date = CX(42932), High = 98},
new HighMetric { Date = CX(42933), High = 95},
new HighMetric { Date = CX(42934), High = 97},
new HighMetric { Date = CX(42935), High = 97},
new HighMetric { Date = CX(42936), High = 99},
new HighMetric { Date = CX(42937), High = 100},
new HighMetric { Date = CX(42938), High = 100},
new HighMetric { Date = CX(42939), High = 100},
new HighMetric { Date = CX(42940), High = 91},
new HighMetric { Date = CX(42941), High = 97},
new HighMetric { Date = CX(42942), High = 98},
new HighMetric { Date = CX(42943), High = 101},
new HighMetric { Date = CX(42944), High = 104},
new HighMetric { Date = CX(42945), High = 98},
new HighMetric { Date = CX(42946), High = 94},
new HighMetric { Date = CX(42947), High = 92},
new HighMetric { Date = CX(42948), High = 93},
new HighMetric { Date = CX(42949), High = 84},
new HighMetric { Date = CX(42950), High = 93},
new HighMetric { Date = CX(42951), High = 97},
new HighMetric { Date = CX(42952), High = 97},
new HighMetric { Date = CX(42953), High = 99},
new HighMetric { Date = CX(42954), High = 94},
new HighMetric { Date = CX(42955), High = 90},
new HighMetric { Date = CX(42956), High = 91},
new HighMetric { Date = CX(42957), High = 99},
new HighMetric { Date = CX(42958), High = 101},
new HighMetric { Date = CX(42959), High = 97},
new HighMetric { Date = CX(42960), High = 91},
new HighMetric { Date = CX(42961), High = 92},
new HighMetric { Date = CX(42962), High = 96},
new HighMetric { Date = CX(42963), High = 96},
new HighMetric { Date = CX(42964), High = 92},
new HighMetric { Date = CX(42965), High = 94},
new HighMetric { Date = CX(42966), High = 99},
new HighMetric { Date = CX(42967), High = 98},
new HighMetric { Date = CX(42968), High = 96},
new HighMetric { Date = CX(42969), High = 98},
new HighMetric { Date = CX(42970), High = 91},
new HighMetric { Date = CX(42971), High = 90},
new HighMetric { Date = CX(42972), High = 88},
new HighMetric { Date = CX(42973), High = 83},
new HighMetric { Date = CX(42974), High = 83},
new HighMetric { Date = CX(42975), High = 88},
new HighMetric { Date = CX(42976), High = 90},
new HighMetric { Date = CX(42977), High = 89},
new HighMetric { Date = CX(42978), High = 90},
new HighMetric { Date = CX(42979), High = 92},
new HighMetric { Date = CX(42980), High = 94},
new HighMetric { Date = CX(42981), High = 95},
new HighMetric { Date = CX(42982), High = 95},
new HighMetric { Date = CX(42983), High = 93},
new HighMetric { Date = CX(42984), High = 85},
new HighMetric { Date = CX(42985), High = 86},
new HighMetric { Date = CX(42986), High = 85},
new HighMetric { Date = CX(42987), High = 87},
new HighMetric { Date = CX(42988), High = 86},
new HighMetric { Date = CX(42989), High = 87},
new HighMetric { Date = CX(42990), High = 85},
new HighMetric { Date = CX(42991), High = 91},
new HighMetric { Date = CX(42992), High = 98},
new HighMetric { Date = CX(42993), High = 94},
new HighMetric { Date = CX(42994), High = 95},
new HighMetric { Date = CX(42995), High = 95},
new HighMetric { Date = CX(42996), High = 94},
new HighMetric { Date = CX(42997), High = 97},
new HighMetric { Date = CX(42998), High = 98},
new HighMetric { Date = CX(42999), High = 97},
new HighMetric { Date = CX(43000), High = 97},
new HighMetric { Date = CX(43001), High = 95},
new HighMetric { Date = CX(43002), High = 93},
new HighMetric { Date = CX(43003), High = 93},
new HighMetric { Date = CX(43004), High = 91},
new HighMetric { Date = CX(43005), High = 92},
new HighMetric { Date = CX(43006), High = 81},
new HighMetric { Date = CX(43007), High = 73},
new HighMetric { Date = CX(43008), High = 86},
new HighMetric { Date = CX(43009), High = 89},
new HighMetric { Date = CX(43010), High = 91},
new HighMetric { Date = CX(43011), High = 88},
new HighMetric { Date = CX(43012), High = 88},
new HighMetric { Date = CX(43013), High = 88},
new HighMetric { Date = CX(43014), High = 89},
new HighMetric { Date = CX(43015), High = 89},
new HighMetric { Date = CX(43016), High = 91},
new HighMetric { Date = CX(43017), High = 94},
new HighMetric { Date = CX(43018), High = 72},
new HighMetric { Date = CX(43019), High = 77},
new HighMetric { Date = CX(43020), High = 87},
new HighMetric { Date = CX(43021), High = 92},
new HighMetric { Date = CX(43022), High = 93},
new HighMetric { Date = CX(43023), High = 81},
new HighMetric { Date = CX(43024), High = 75},
new HighMetric { Date = CX(43025), High = 76},
new HighMetric { Date = CX(43026), High = 83},
new HighMetric { Date = CX(43027), High = 82},
new HighMetric { Date = CX(43028), High = 84},
new HighMetric { Date = CX(43029), High = 89},
new HighMetric { Date = CX(43030), High = 75},
new HighMetric { Date = CX(43031), High = 83},
new HighMetric { Date = CX(43032), High = 74},
new HighMetric { Date = CX(43033), High = 80},
new HighMetric { Date = CX(43034), High = 85},
new HighMetric { Date = CX(43035), High = 67},
new HighMetric { Date = CX(43036), High = 60},
new HighMetric { Date = CX(43037), High = 78},
new HighMetric { Date = CX(43038), High = 77},
new HighMetric { Date = CX(43039), High = 61},
new HighMetric { Date = CX(43040), High = 80},
new HighMetric { Date = CX(43041), High = 92},
new HighMetric { Date = CX(43042), High = 74},
new HighMetric { Date = CX(43043), High = 88},
new HighMetric { Date = CX(43044), High = 92},
new HighMetric { Date = CX(43045), High = 75},
new HighMetric { Date = CX(43046), High = 62},
new HighMetric { Date = CX(43047), High = 50},
new HighMetric { Date = CX(43048), High = 54},
new HighMetric { Date = CX(43049), High = 64},
new HighMetric { Date = CX(43050), High = 70},
new HighMetric { Date = CX(43051), High = 71},
new HighMetric { Date = CX(43052), High = 67},
new HighMetric { Date = CX(43053), High = 79},
new HighMetric { Date = CX(43054), High = 74},
new HighMetric { Date = CX(43055), High = 75},
new HighMetric { Date = CX(43056), High = 82},
new HighMetric { Date = CX(43057), High = 76},
new HighMetric { Date = CX(43058), High = 63},
new HighMetric { Date = CX(43059), High = 66},
new HighMetric { Date = CX(43060), High = 70},
new HighMetric { Date = CX(43061), High = 56},
new HighMetric { Date = CX(43062), High = 68},
new HighMetric { Date = CX(43063), High = 78},
new HighMetric { Date = CX(43064), High = 75},
new HighMetric { Date = CX(43065), High = 73},
new HighMetric { Date = CX(43066), High = 76},
new HighMetric { Date = CX(43067), High = 78},
new HighMetric { Date = CX(43068), High = 66},
new HighMetric { Date = CX(43069), High = 76},
new HighMetric { Date = CX(43070), High = 73},
new HighMetric { Date = CX(43071), High = 78},
new HighMetric { Date = CX(43072), High = 77},
new HighMetric { Date = CX(43073), High = 82},
new HighMetric { Date = CX(43074), High = 58},
new HighMetric { Date = CX(43075), High = 55},
new HighMetric { Date = CX(43076), High = 51},
new HighMetric { Date = CX(43077), High = 48},
new HighMetric { Date = CX(43078), High = 60},
new HighMetric { Date = CX(43079), High = 72},
new HighMetric { Date = CX(43080), High = 77},
new HighMetric { Date = CX(43081), High = 57},
new HighMetric { Date = CX(43082), High = 75},
new HighMetric { Date = CX(43083), High = 62},
new HighMetric { Date = CX(43084), High = 56},
new HighMetric { Date = CX(43085), High = 63},
new HighMetric { Date = CX(43086), High = 52},
new HighMetric { Date = CX(43087), High = 53},
new HighMetric { Date = CX(43088), High = 61},
new HighMetric { Date = CX(43089), High = 61},
new HighMetric { Date = CX(43090), High = 77},
new HighMetric { Date = CX(43091), High = 68},
new HighMetric { Date = CX(43092), High = 51},
new HighMetric { Date = CX(43093), High = 51},
new HighMetric { Date = CX(43094), High = 51},
new HighMetric { Date = CX(43095), High = 32},
new HighMetric { Date = CX(43096), High = 25},
new HighMetric { Date = CX(43097), High = 32},
new HighMetric { Date = CX(43098), High = 33},
new HighMetric { Date = CX(43099), High = 34},
new HighMetric { Date = CX(43100), High = 35},

          };

        private readonly ILogger _logger;

        public DashboardsController(
            ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        //
        // GET: /Dashboards[?Period=Day&PeriodCount20=&Category=All]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index(string Period = "Day", int PeriodCount = 20, string Category = "All")
        {
            var periodHighs =
                from metric in highs
                group metric by BuildGroupKey(Period.ToUpper(), metric.Date) into g
                select new
                {
                    Label = g.Key,
                    Highs = g.Average(m => m.High)
                };

            var count2 = periodHighs.Count();
            var temps = periodHighs.Skip(count2 - PeriodCount).Take(PeriodCount);


            ViewData["Metrics"] = GetJsonMetrics(Period.ToUpper(), PeriodCount, Category.ToUpper());
            ViewData["Highs"] = Newtonsoft.Json.JsonConvert.SerializeObject(temps);
            return View();
        }

        //
        // GET: /Dashboards/Sales?display=&
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Sales(string display)
        {
            var x = display;
            return View("~/Views/Dashboards/Index.cshtml");
        }


        [DataContract]
        public class Metric
        {
            [DataMember]
            public DateTime Date { get; set; }

            [DataMember]
            public Decimal Value { get; set; }

            [DataMember]
            public string Type { get; set; }

            [DataMember]
            public string Color { get; set; }

            [DataMember]
            public string Divsion { get; set; }
        }

        private string BuildGroupKey(string period, DateTime date)
        {
            if(period == "DAY")
            {
                return date.ToString("M/d");
            } 

            if(period == "MONTH")
            {
                return date.ToString("M/yy");
            }

            if (period == "WEEK")
            {
                int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
                var weekStarting = date.AddDays(-1 * diff).Date;
                return weekStarting.ToString("M/d");
            }

            if (period == "QUARTER")
            {
                var YY = date.ToString("yy");
                var quarter = (date.Month % 4) + 1;
                return quarter.ToString() + "/" + YY;
            }

            if (period == "YEAR")
            {
                return date.ToString("yyyy");
            }

            return date.ToString();
        }
        public string GetJsonMetrics(string Period, int PeriodCount, string Category)
        {
            var metrics = new List<Metric>();
            /*
            string cs = "Server=tcp:mdewig-sandbox.database.windows.net,1433;Initial Catalog=Database;Persist Security Info=False;User ID= analyst;Password=Qmp1=79Rst34;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            
            using (SqlConnection con = new SqlConnection(cs))
            {
                var cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[m].[usp_select_metric]";
                cmd.Parameters.AddWithValue("@fact_id", 3);
                cmd.Parameters.AddWithValue("@start_date", "2016-01-01");
                cmd.Parameters.AddWithValue("@end_date", "2017-12-31");
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Metric metric = new Metric();
                    metric.Date = Convert.ToDateTime(rdr["date"]);
                    metric.Value = Convert.ToDecimal(rdr["value"]);
                    metric.Type = rdr["Type"].ToString();
                    metric.Color = rdr["Color"].ToString();
                    metric.Divsion = rdr["Division"].ToString();
                    metrics.Add(metric);
                }
            }
            
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(metrics);
            System.IO.File.WriteAllText(@"path.txt", json);
            */
            var j = System.IO.File.ReadAllText(@"path.txt");
            metrics = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Metric>>(j);

            
            var periodMetrics =
                from metric in metrics
                group metric by BuildGroupKey(Period, metric.Date) into g
                select new
                {
                    Label = g.Key,
                    Sales = g.Sum(m => m.Value)
                };

            var count = periodMetrics.Count();
            var sales = periodMetrics.Skip(count - PeriodCount).Take(PeriodCount);

            /*
            var sales =
                from metric in metrics
                group metric by metric.Date into metricGroup
                select new
                {
                    Date = metricGroup.Key,
                    Sales = metricGroup.Sum(x => x.Value),
                };
            */
            return Newtonsoft.Json.JsonConvert.SerializeObject(sales);
        }
    }
}
