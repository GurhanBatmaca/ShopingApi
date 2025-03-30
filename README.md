# 🎟 Etkinlik Biletleri Alım Satım API

**ShopApi**, **ASP.NET Core Web API** kullanılarak geliştirilen modern bir e-ticaret API'sidir. API, **katmanlı mimari** yaklaşımına göre yapılandırılmıştır. Kullanıcı doğrulaması ve rol tabanlı yetkilendirme **JWT Bearer Token** kullanılarak yapılır. **Entity Framework** veri erişimi için, **Microsoft SQL Server** ise veritabanı olarak kullanılmaktadır.

## 🚀 Teknolojiler

- **ASP.NET Core 8.0**
- **Katmanlı Mimari**
- **Entity Framework Core**
- **Entity Framework Identity**
- **Authentication ve Role-Based Authorization**
- **JWT (JSON Web Token) Bearer Token**
- **Microsoft SQL Server**

---

## 📌 API Endpoints

Aşağıda API'de kullanılan endpoint'ler ve HTTP istek metotları listelenmiştir:


| HTTP Verbs | Endpoint                                                             | Açıklama                                       |
| ---------- | -------------------------------------------------------------------- | ---------------------------------------------- |
| **POST**   | `/api/auth/register`                                                 | Yeni bir kullanıcı hesabı oluşturur.           |
| **POST**   | `/api/auth/login`                                                    | Var olan bir kullanıcı hesabıyla giriş yapar.  |
| **GET**    | `/api/products`                                                      | Tüm ürünleri listeler.                        |
| **GET**    | `/api/products/{id}`                                                 | Belirli bir ürünü getirir.                     |
| **POST**   | `/api/products`                                                      | Yeni bir ürün ekler (Admin rolü gerektirir).   |
| **PUT**    | `/api/products/{id}`                                                 | Mevcut bir ürünü günceller (Admin rolü gerektirir). |
| **DELETE** | `/api/products/{id}`                                                 | Bir ürünü siler (Admin rolü gerektirir).       |
| **GET**    | `/api/products?page=1`                                               | Ürünleri sayfalı olarak getirir.               |
| **GET**    | `/api/products/search?name=telefon&category=elektronik&page=1`       | Belirtilen kriterlere göre ürünleri arar.      |
| **DELETE** | `/api/admin/deleteproduct/{id}`                                      | Bir ürünü admin yetkisiyle siler.              |


## 🔐 API Yanıt Örnekleri

### 1️⃣ **Kimlik Doğrulama Yanıtı**

```json
{
    "data": {
        "token": "{Token}",
        "expireDate": "{Token expire date}"
    }
}
```


### 1️⃣ **Ürün İsteme Yanıtı**

{
    "data": [
        {
            "id": 1,
            "name": "Ürün A",
            "price": 100,
            "category": "Elektronik"
        },
        {
            "id": 2,
            "name": "Ürün B",
            "price": 150,
            "category": "Ev Eşyası"
        }
    ],
    "pageInfo": {
        "totalItems": 100,
        "itemPerPage": 2,
        "currentPage": 1,
        "totalPages": 50
    }
}

---

## 📌 Kurulum ve Çalıştırma

1. **Projeyi Klonlayın:**
   ```sh
   git clone https://github.com/gurhanbatmaca/event-ticket-api.git
   cd event-ticket-api
   ```
2. **appsettings.json Dosyasını Yapılandırın**
   
3. **Bağımlılıkları Yükleyin:**
   ```sh
   dotnet restore
   ```
4. **Veritabanı Göçlerini Çalıştırın:**
   ```sh
   dotnet ef database update
   ```
5. **Uygulamayı Başlatın:**
   ```sh
   dotnet run
   ```

---

## 🛠 Katkıda Bulunma

Her türlü katkıya açığız! Fork alıp geliştirme yapabilir ve pull request gönderebilirsiniz. Sorularınız için [Gürhan Batmaca](https://github.com/gurhanbatmaca) ile iletişime geçebilirsiniz.

🚀 **İyi kodlamalar!**
 
