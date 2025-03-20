
Kurallar
* Tüm servis ve repoların çağırıldığı yerde tanımlamaları _ ile başlar ve Camel Case devam eder. Örn: _userService

* Tüm Model İsimlerndirmeleri Pascal Case. 

* Tüm Modellerin yeni instanceları Camel Case.

* Tüm database ve dto propertyleri Snake Case.

* Tüm Methodlar Pascal Case.





Yazılımda İsimlendirme Çeşitleri
Camel case: isimlendirme türünde ilk harf küçük harf ile başlar ve sonraki her yeni sözcüğün ilk harfi büyük olmalıdır.

Örnek: buttonPrimaryHover

Snake Case: isimlendirme türünde her yeni sözcüğün arası ‘_’ (alt çizgi) ile ayrılır, ve her kelime küçük harf ile başlar.

Örnek: button_primary_hover

Kebap Case: isimlendirme türünde ise her yeni sözcüğün arası ‘-‘ (tire) ile ayrılır, ve her kelime küçük harf ile başlar.

Örnek: button-primary-hover

Pascal Case: isimlendirme türü ise camel case ile aynı sayılabilir, aralarındaki tek fark camel case ‘de ilk kelimenin ilk harfi küçük iken, Pascal case ‘de Her kelimenin ilk harfi büyük yazılır.

Örnek: ButtonPrimaryHover

Upper Case: isimlendirme türünde bütün kelimeler büyük harfle yazılır ve kelimelerin arası ‘_’ (alt çizgi) ile ayrılır.

Örnek: BUTTON_PRIMARY_HOVER



Interface Kullanımları
Get : Tek bir model getirme 
GetList : Liste halinde döner
AddOrUpdate : Ekleme ve güncelleme için tek bir method çalışır.
Delete : Silme için