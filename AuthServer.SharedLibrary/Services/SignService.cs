using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthServer.SharedLibrary.Services
{
    public static class SignService
    {
        // Bu metot, simmetrik bir təhlükəsizlik açarı yaradır.
        // Verilən təhlükəsizlik açarı string'ini istifadə edərək
        // SymmetricSecurityKey obyektini qaytarır.
        // Bu metod, əsasən JWT (JSON Web Token) kimi təhlükəsizlik əməliyyatlarında
        // istifadə olunan simmetrik açarları yaratmaq üçün istifadə olunur.
        //
        // Nümunə İstifadə:
        //   string mySecurityKey = "gizli_anahtar";
        //   SecurityKey securityKey = SignService.GetSymmetricSecurityKey(mySecurityKey);
        //
        // Qeyd: Bu nümunə, təhlükəsizlik məqsədi üçün tətbiq hazırlarkən istifadə oluna bilər.
        // Amma, həqiqi tətbiqlərdə təhlükəsizlik açarları diqqətlə qorunmalıdır.
        // Əlavə olaraq, təhlükəsizlik açarları açıq bir şəkildə kodda olmalıdır,
        // ya da daha təhlükəsiz bir şəkildə idarə oluna bilər məsələsi tətbiq tələblərinə bağlıdır.
        public static SecurityKey GetSymMetricSecurityKey(string securityKey)
        {
            // SymmetricSecurityKey obyektini yaratmaq və bu bayt sırasını açar kimi təyin etmək.Ve
            // Verilən təhlükəsizlik açarını UTF-8 kodlaması əsasında bayt sırasına çevirir. 
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
