# Domain-Driven Design Eğitim Projesi | .NET 8

## Proje Özeti

Bu projede .NET 8 kullanarak Domain-Driven Design'ın temel yapı taşları olan
Entity, Value Object, Aggregate, Repository, Unit of Work, Domain Event ve CQRS
yaklaşımlarını uygulamalı olarak çalıştım.

## Proje hakkında

Bu proje, Domain-Driven Design (DDD) yaklaşımını .NET ekosistemi üzerinde uygulamalı olarak öğrenmek amacıyla geliştirilmiştir. Proje kapsamında kullanıcı, kategori, ürün ve sipariş süreçlerinden oluşan örnek bir e-ticaret domaini; katmanlı mimari, CQRS, MediatR ve domain odaklı modelleme prensipleri kullanılarak ele alınmıştır.

## Projenin amacı

Amaç; iş kurallarını teknik ayrıntılardan ayırmak, domain modelini davranış içeren nesnelerle ifade etmek ve uygulama ile veri erişim katmanlarının Domain katmanına bağımlı olduğu bir yapı kurmaktır. Projede ayrıca komut/sorgu ayrımı, domain event'leri ve Entity Framework Core eşlemeleri pratik edilmiştir.

## Öne Çıkan Özellikler

- Domain Entity ve Value Object modelleme
- Aggregate Root kullanımı
- Repository ve Unit of Work pattern
- CQRS + MediatR
- Domain Events
- EF Core Value Object mapping
- SQL Server persistence
- Swagger API documentation

## Kullanılan teknolojiler

- .NET 8 ve C#
- ASP.NET Core Web API (Controller tabanlı)
- Entity Framework Core 8 ve SQL Server
- MediatR
- Swagger / Swashbuckle
- BenchmarkDotNet (bağımsız ConsoleApp denemeleri için)

## Proje mimarisi ve klasör yapısı

```text
DomainDrivenDesign/
├── DomainDrivenDesign.Domain/          # Entity'ler, value object'ler, iş kuralları ve sözleşmeler
├── DomainDrivenDesign.Application/     # Command/query modelleri ve MediatR handler'ları
├── DomainDrivenDesign.Infrastructure/  # EF Core, SQL Server, migration ve repository uygulamaları
├── DomainDrivenDesign.WebApi/          # API controller'ları, Swagger ve uygulama başlangıcı
├── DomainDrivenDesign.ConsoleApp/      # Entity eşitliği, event ve benchmark denemeleri
└── DomainDrivenDesign.slnx
```

Temel bağımlılık yönleri `WebApi -> Application -> Domain` ve `WebApi -> Infrastructure -> Domain` şeklindedir. Application katmanı işlemleri repository arayüzleri üzerinden yürütür; Infrastructure katmanı bu arayüzleri EF Core ile uygular. Web API ise MediatR aracılığıyla command ve query'leri ilgili handler'lara iletir.

## Uygulanan Domain-Driven Design kavramları

| Kavram | Projedeki karşılığı |
| --- | --- |
| Entity | `Entity` temel sınıfı kimlik tabanlı eşitlik sağlar; `User`, `Category`, `Product`, `Order` ve `OrderLine` bu yapıyı kullanır. |
| Value Object | `Name`, `Email`, `Password`, `Address`, `Money` ve `Currency` record olarak modellenmiştir. Doğrulama ve değer tabanlı eşitlik bu tiplerde tutulur. |
| Aggregate | `Order`, `OrderLine` koleksiyonunu kendi davranışları üzerinden oluşturan ve değiştiren aggregate kökü rolündedir. |
| Repository | Arayüzler Domain katmanında, EF Core uygulamaları Infrastructure katmanındadır. |
| Unit of Work | `IUnitOfWork` ve `ApplicationDbContext`, değişikliklerin tek noktadan kaydedilmesini sağlar. |
| Factory | `User.CreateUser`, geçerli value object'lerle bir kullanıcı oluşturmak için statik factory metodu olarak kullanılır. |
| Domain Event | Kullanıcı ve sipariş oluşturulduktan sonra `UserDomainEvent` ve `OrderDomainEvent` MediatR ile yayımlanır. E-posta/SMS handler'ları örnek amaçlıdır ve gerçek bir dış servis entegrasyonu içermez. |
| CQRS | Oluşturma işlemleri command, listeleme işlemleri query olarak feature bazlı klasörlerde ayrılmıştır. |

## Projenin çalıştırılması

Gereksinimler:

- .NET 8 SDK
- SQL Server
- Migration uygulamak için `dotnet-ef` aracı

`DomainDrivenDesign/DomainDrivenDesign.Infrastructure/Context/ApplicationDbContext.cs` içindeki SQL Server bağlantı cümlesini kendi ortamınıza göre düzenleyin. Ardından repository kökünde şu komutları çalıştırın:

```bash
cd DomainDrivenDesign
dotnet restore DomainDrivenDesign.WebApi/DomainDrivenDesign.WebApi.csproj
dotnet ef database update --project DomainDrivenDesign.Infrastructure --startup-project DomainDrivenDesign.WebApi
dotnet run --project DomainDrivenDesign.WebApi
```

Development profiliyle Swagger arayüzüne `http://localhost:5181/swagger` adresinden ulaşılabilir.

## Bu projede neler öğrendim / uyguladım?

- Entity ve value object ayrımını, kimlik ve değer tabanlı eşitliği modellemeyi
- İş kurallarını domain nesneleri içinde tutmayı ve nesne durumunu davranışlar üzerinden değiştirmeyi
- Aggregate, repository, factory, unit of work ve domain event yaklaşımlarını
- CQRS ile command/query akışlarını MediatR üzerinden yönetmeyi
- Value object'leri EF Core conversion ve owned type eşlemeleriyle SQL Server'a kaydetmeyi
- Katmanlar arasında bağımlılıkları DI üzerinden kurmayı

## 🙏 Referans

Kodlar ve içerik, **Taner Saydam** hocanın Udemy kursu  
➡️ [Domain-Driven Design'ı Uygulamalı Öğrenelim](https://www.udemy.com/course/domain-driven-design-uygulamali-ogrenelim/) eğitimi takip edilerek geliştirilmiştir.

DDD yaklaşımını daha iyi anlamamı sağlayan değerli anlatımı için kendisine teşekkür ederim. 🙏

[![Taner Saydam](https://img.shields.io/badge/Instructor-Taner%20Saydam-blue?style=for-the-badge&logo=github)](https://github.com/TanerSaydam)