using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ThrottlR
{
    /// <summary>
    /// Delegate der Funktion für das Blockieren der Requests
    /// (Da es sich bei Delegaten um Referenzen auf Funktionen handelt, müssen die Signaturen stets
    /// übereinstimmen.)
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public delegate Task AttackDetectedDelegate(HttpContext context);
}
