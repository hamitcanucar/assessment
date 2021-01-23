# assessment

Merhaba 

Projeden biraz bahsetmem gerekirse,

Öncelikle sisteme kayıt olma durumunu ele aldım ve kullanıcı gerekli bilgileri girerek sisteme kayıt oluyor. Daha sonrasında kullanıcı adı ve şifre bilgisi ile sisteme giriş yapıyor ve rehberine istediği kadar kayıt girebiliyor. Girmiş olduğu kayıtları listeleyebiliyor ve silebiliyor. Kullanıcı aynı zamanda kendi bilgilerini ve şifre değiştirme işlemini yapabiliyor. Kullanıcı sınırsız kayıt girebiliyor. Daha sonrasında lokasyona göre kullanıcı rapor talebinde bulunabiliyor. Bu aşamada Kafka vb. bir message queue kullanılması istenmişti bende RabbitMQ kullanarak istemiş olduğunuz yapıyı kurmaya çalıştım. Rapor create edildikten sonra response'da bir raporId dönüyor. Bir veya birden falza rapor talebinde bulunulabilir. RabbitMQ kullanmamın nedeni RabbitMQ konfigürasyonlarında herhangi bir değişiklik yapmadan kuyruklara bağlanıp mesajları işlemeye başlayabiliyor. Tabi bunun gibi bir çok avantajı da beraberinde getiriyor. 

doc klasörü içerisinde postman için gerekli json dosyasını bulabilirsiniz. 
