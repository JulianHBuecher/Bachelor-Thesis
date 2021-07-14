using Microsoft.AspNetCore.Http;
using System.Text;
using System.Threading.Tasks;

namespace ThrottlR
{
    public class ThrottleMLOptions : ThrottleOptions
    {
        private static readonly byte[] _attackDetectedMessage = Encoding.UTF8.GetBytes("You have tried to attack this application.");

        // Erweiterung für das Handling von identifizierten Angriffen auf die Anwendung
        public AttackDetectedDelegate OnAttackOccured { get; set; } = DefaultAttackDetectedDelegate;

        private static Task DefaultAttackDetectedDelegate(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            return context.Response.Body.WriteAsync(_attackDetectedMessage, 0, _attackDetectedMessage.Length);
        }
    }
}
