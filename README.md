# Asp.Net Core 6 MVC CodeFirst - Identity Kütüphanesi Mesajlaşma Uygulama Projesi
M&Y Yazılım Akademi Danışmanlıktan aldığım eğitim kapsamında geliştirmiş olduğum 4. proje olan Identity Kütüphanesi kullanılarak geliştirilen mesajlaşma uygulaması projesidir. Proje amacı kişilerin üyelik oluşturup sistemdeki üyelerle mesajlaşabildikleri, mesajlarını yönetebildikleri bir sistemi içermektedir. Bununla birlikte bu kullanıcıları yöneten Yönetici rolüyle de kullanıcıların onayları, sistemden silinmeleri, bilgileri ve sistemdeki istatistiki bilgileri görüntülenebilmektedir. 
## Projeye Ait Bazı Özellikler;
* Proje Asp.Net 6.0 MVC mimarisinde CodeFirst yaklaşımıyla yapıldı.
* Identity kütüphanesiyle login-logout-register-role sistemi kullanıldı.
* Projede bol bol iç içe layout ve ViewComponent yapısı kullanıldı.
* Proje Admin ve User olmak üzere 2 Areaya sahiptir, giriş yapan kullanıcın rolüne göre sistem ilgili Areya yönlendirmektedir.
* Navbarda gelen okunmamış mesajlar ve sayıları gösterilmektedir.
* Kullanıcı kendi profil bilgilerini kendisi güncelleyebilmekte ve şifresini eski şifresini de bilerek kontrollü bir şekilde onaylayarak değiştirebilmektedir.
* Kullanıcılar profil fotoğraflarını sisteme dahil edebilmektedirler. İlk üyelik sırasında alınmayan profil fotoğrafı yerine yeni kullanıcıya varsayılan olarak bir görsel atanmaktadır.
* Onaylanmamış kullanıcı sisteme giriş yaptıktan sonra bir hata ekranıyla karşılaşıp ana ekrana yönlendirilmektedir ve oturumu kapatılmaktadır.
* Kullanıcıların yaş bilgisinden sistemdeki en yaşlı ve en genç kullanıcılar tespit edilip yönetici panelindeki dashboard ekranında istatistik olarak görüntülenebilmektedir.
* Yönetici rolündeki kullanıcı dashboard üzerinden sistem istatistiklerini görüntüleyebilmektedir.
* Yöneticiler onaylayabilmekte ve rollerini değiştirebilmekte, sisteme rol dahil edip, bu rolleri silebilmektedirler. Kullanıcı rolleri güncellenirken bu eklenen bütün roller dropdownlist formatında açılan pop-up içerisine dinamik olarak gelmektedir.
* Mesajlar panelinde mesajlar birden fazla olarak TOPLU halde silinebilir, önemli veya çöp klasörüne atılabilir.
* Sistemdeki oluşabilecek bütün mantık hataları kontrol edildi ve giderildi, kontrollü ve kullanıcı odaklı çalışması sağlandı.
* Projede diğer teknolojilerle birlikte yoğunluklu olarak AJAX - Javascript yapısı kullanılmıştır.

### Kullanılan Bazı Teknolojiler
* Structural Repository design pattern ile oluşturulmuştur.
* DbCodeFirst ile MSSQL veritabanı oluşturulup yönetimi sağlandı.
* Identity kütüphanesiyle login-logout-role-register sistemi kullanıldı.
* Entity Framework 6.0 Veritabanı etkileşimi ve ORM için kullanıldı.
* AJAX - JAVASCRIPT ile sayfa içinde açılan paneller, alert ve CRUD işlemleri yapıldı.
* Area sistemiyle paneller birbirinden ayrılıp yönetimi kolaylaştırıldı.
* Dto katmanıyla veri yönetimi kolaylaştırıldı.
* HTML-CSS Bootstrap ile arayüzler tasarlandı.
* Fluent Validation - kontrol sistemi kullanılarak veirlerin belli kurallara göre alınması sağlandı.
* ViewBaglerle verilerin taşınması.
* 403 - 404 sayfalarının bulunması.
* Authentication - authorize oturum yönetim sistemi.
* AutoMapper nesneyle nesnenin eşleşmesi sağlandı.
* Login sistemi
* Linq sorguları


# Veritabanı
![Veritabanı](https://github.com/batuhanyalin/IdentityMessagingApplication/blob/master/IdentityMessagingApplication.PresentationLayer/wwwroot/images/projectScreenshots/database.png?raw=true)
# Giriş
![Dashboard Mesaj Bildirimi](https://github.com/batuhanyalin/IdentityMessagingApplication/blob/master/IdentityMessagingApplication.PresentationLayer/wwwroot/images/projectScreenshots/login.png?raw=true)
# Onay Bekleyen Kullanıcı
![Dashboard Mesaj Bildirimi](https://github.com/batuhanyalin/IdentityMessagingApplication/blob/master/IdentityMessagingApplication.PresentationLayer/wwwroot/images/projectScreenshots/onay.png?raw=true)
# Yetkisiz Giriş
![Dashboard Mesaj Bildirimi](https://github.com/batuhanyalin/IdentityMessagingApplication/blob/master/IdentityMessagingApplication.PresentationLayer/wwwroot/images/projectScreenshots/error403forbidden.png?raw=true)
# Sayfa Bulunamadı
![Dashboard Mesaj Bildirimi](https://github.com/batuhanyalin/IdentityMessagingApplication/blob/master/IdentityMessagingApplication.PresentationLayer/wwwroot/images/projectScreenshots/error404.png?raw=true)
# Yeni Üyelik
![Dashboard Mesaj Bildirimi](https://github.com/batuhanyalin/IdentityMessagingApplication/blob/master/IdentityMessagingApplication.PresentationLayer/wwwroot/images/projectScreenshots/registerValidation.png?raw=true)

#### Dashboard
![Dashboard](https://github.com/batuhanyalin/IdentityMessagingApplication/blob/master/IdentityMessagingApplication.PresentationLayer/wwwroot/images/projectScreenshots/dashboard.png?raw=true)
#### Kullanıcı Listesi
![Kullanıcı Listesi](https://github.com/batuhanyalin/IdentityMessagingApplication/blob/master/IdentityMessagingApplication.PresentationLayer/wwwroot/images/projectScreenshots/userList.png?raw=true)
#### Kullanıcı Silme
![Profilim](https://github.com/batuhanyalin/IdentityMessagingApplication/blob/master/IdentityMessagingApplication.PresentationLayer/wwwroot/images/projectScreenshots/deleteUser.png?raw=true)
#### Kullanıcı Güncelleme
![Kullanıcı Güncelleme](https://github.com/batuhanyalin/IdentityMessagingApplication/blob/master/IdentityMessagingApplication.PresentationLayer/wwwroot/images/projectScreenshots/updateUser.png?raw=true)
##### Kullanıcı Detay
![Bütün Mesajlar](https://github.com/batuhanyalin/IdentityMessagingApplication/blob/master/IdentityMessagingApplication.PresentationLayer/wwwroot/images/projectScreenshots/userDetail.png?raw=true)
#### Profilim - Kullanıcı Paneli
![Profilim](https://github.com/batuhanyalin/IdentityMessagingApplication/blob/master/IdentityMessagingApplication.PresentationLayer/wwwroot/images/projectScreenshots/myProfileUserPanel.png?raw=true)
#### Şifre Değiştirme
![Profilim](https://github.com/batuhanyalin/IdentityMessagingApplication/blob/master/IdentityMessagingApplication.PresentationLayer/wwwroot/images/projectScreenshots/updatePassword.png?raw=true)
#### Şifre Değiştirme 
###### Güncel Parola Yanlış
![Profilim](https://github.com/batuhanyalin/IdentityMessagingApplication/blob/master/IdentityMessagingApplication.PresentationLayer/wwwroot/images/projectScreenshots/updatePasswordError.png?raw=true)
#### Şifre Değiştirme
###### Girilen Şifreler Birbirleriyle Eşleşmiyor
![Profilim](https://github.com/batuhanyalin/IdentityMessagingApplication/blob/master/IdentityMessagingApplication.PresentationLayer/wwwroot/images/projectScreenshots/updatePasswordError2.png?raw=true)
#### Gelen Mesajlar
![Profilim](https://github.com/batuhanyalin/IdentityMessagingApplication/blob/master/IdentityMessagingApplication.PresentationLayer/wwwroot/images/projectScreenshots/inboxMessageList.png?raw=true)
#### Gelen Mesajlar - Kullanıcı Paneli
![Profilim](https://github.com/batuhanyalin/IdentityMessagingApplication/blob/master/IdentityMessagingApplication.PresentationLayer/wwwroot/images/projectScreenshots/inboxUserPanel.png?raw=true)
#### Gönderilen Mesajlar
![Profilim](https://github.com/batuhanyalin/IdentityMessagingApplication/blob/master/IdentityMessagingApplication.PresentationLayer/wwwroot/images/projectScreenshots/sentMessageList.png?raw=true)
#### Taslak Mesajlar
![Profilim](https://github.com/batuhanyalin/IdentityMessagingApplication/blob/master/IdentityMessagingApplication.PresentationLayer/wwwroot/images/projectScreenshots/draftMessageList.png?raw=true)
#### Mesaj Okuma
![Profilim](https://github.com/batuhanyalin/IdentityMessagingApplication/blob/master/IdentityMessagingApplication.PresentationLayer/wwwroot/images/projectScreenshots/readMessage.png?raw=true)
#### Mesajları Çöp Kutusuna Taşı
![Profilim](https://github.com/batuhanyalin/IdentityMessagingApplication/blob/master/IdentityMessagingApplication.PresentationLayer/wwwroot/images/projectScreenshots/moveTrash.png?raw=true)
#### Mesajlar Çöp Kutusuna Taşındı
![Profilim](https://github.com/batuhanyalin/IdentityMessagingApplication/blob/master/IdentityMessagingApplication.PresentationLayer/wwwroot/images/projectScreenshots/correctTrash.png?raw=true)
#### Mesajlar Çöp Kutusundan Alma
![Profilim](https://github.com/batuhanyalin/IdentityMessagingApplication/blob/master/IdentityMessagingApplication.PresentationLayer/wwwroot/images/projectScreenshots/moveInbox.png?raw=true)
#### Mesajları Önemli Kutusuna Taşı
![Profilim](https://github.com/batuhanyalin/IdentityMessagingApplication/blob/master/IdentityMessagingApplication.PresentationLayer/wwwroot/images/projectScreenshots/moveImport.png?raw=true)
#### Mesajlar Önemli Kutusuna Taşındı
![Profilim](https://github.com/batuhanyalin/IdentityMessagingApplication/blob/master/IdentityMessagingApplication.PresentationLayer/wwwroot/images/projectScreenshots/correctImport.png?raw=true)
#### Yeni Mesaj Gönderme İşlemi
![Profilim](https://github.com/batuhanyalin/IdentityMessagingApplication/blob/master/IdentityMessagingApplication.PresentationLayer/wwwroot/images/projectScreenshots/correctSendingMessageUserPanel.png?raw=true)
#### Yeni Mesaj Gönderme - Kendine Mesaj Gönderemezsin Hatası
![Profilim](https://github.com/batuhanyalin/IdentityMessagingApplication/blob/master/IdentityMessagingApplication.PresentationLayer/wwwroot/images/projectScreenshots/cantSendingYourselfError.png?raw=true)
#### Taslak Mesaj Kaydetme
![Profilim](https://github.com/batuhanyalin/IdentityMessagingApplication/blob/master/IdentityMessagingApplication.PresentationLayer/wwwroot/images/projectScreenshots/editDraftMessageSave.png?raw=true)
#### Taslak Mesajı Gönderme
![Profilim](https://github.com/batuhanyalin/IdentityMessagingApplication/blob/master/IdentityMessagingApplication.PresentationLayer/wwwroot/images/projectScreenshots/editDraftMessageSending.png?raw=true)
##### Yeni Rol Ekleme
![Yeni Rol Ekleme](https://github.com/batuhanyalin/IdentityMessagingApplication/blob/master/IdentityMessagingApplication.PresentationLayer/wwwroot/images/projectScreenshots/newRole.png?raw=true)
##### Rol Güncelleme
![Rol Güncelleme](https://github.com/batuhanyalin/IdentityMessagingApplication/blob/master/IdentityMessagingApplication.PresentationLayer/wwwroot/images/projectScreenshots/updateRole.png?raw=true)
##### Rol Silme
![Rol Silme](https://github.com/batuhanyalin/IdentityMessagingApplication/blob/master/IdentityMessagingApplication.PresentationLayer/wwwroot/images/projectScreenshots/deleteRole.png?raw=true)



