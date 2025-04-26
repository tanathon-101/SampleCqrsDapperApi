# Clean Architecture Overview

โปรเจกต์นี้ใช้หลักการ **Clean Architecture** ในการจัดโครงสร้างโค้ด เพื่อให้แยกความรับผิดชอบของแต่ละชั้น (Layer) ชัดเจน และทำให้ระบบสามารถ Maintain, Test, และ Scale ได้ง่ายในระยะยาว

---

# ✨ Layers อธิบายแต่ละส่วน

## 1. Domain Layer (Core)

**หน้าที่:**
- กำหนดกฎของธุรกิจ (Business Rules)
- ประกอบด้วย Entities, Value Objects, และ Interface (Contract)

**ตัวอย่างที่อยู่ใน Domain:**
- Entity: `Customer`, `Order`
- Interface: `ICustomerRepository`, `IOrderRepository`

**กฎสำคัญ:**
- Domain ต้องไม่พึ่งพา Infrastructure, Application, หรือ Framework ใดๆ
- บริสุทธิ์ที่สุด (Pure C# Class)

---

## 2. Application Layer (Use Cases)

**หน้าที่:**
- ประกอบด้วย Business Logic ที่กำหนด **"จะทำอะไร"**
- ใช้ Command (Action), Query (Request), และ Handler (Process)
- เรียกใช้ Interface ที่อยู่ใน Domain เพื่อทำงาน

**ตัวอย่างที่อยู่ใน Application:**
- Command: `CreateCustomerCommand`, `UpdateOrderCustomerCommand`
- Query: `GetCustomersQuery`, `GetOrdersQuery`
- Handler: `CreateCustomerCommandHandler`, `GetOrdersQueryHandler`

**กฎสำคัญ:**
- Application รู้จักแค่ Domain (ผ่าน Interface)
- ไม่ต้องรู้ว่าข้อมูลมาจากไหน เช่น SQL หรือ API

---

## 3. Infrastructure Layer (External Implementations)

**หน้าที่:**
- เป็นชั้นที่ Implement Interface ของ Domain
- จัดการ Database, Files, Third-party API, Email Service, ฯลฯ

**ตัวอย่างที่อยู่ใน Infrastructure:**
- `CustomerRepository` (ใช้ Dapper Connect SQL Server)
- `OrderRepository`

**กฎสำคัญ:**
- Infrastructure รู้จัก Domain แต่ Domain ไม่รู้จัก Infrastructure
- ทำหน้าที่แค่ Implement ไม่ควรมี Business Logic

---

## 4. WebAPI Layer (Presentation / UI Layer)

**หน้าที่:**
- รับ Request/Response ผ่าน HTTP (Controller)
- แปลง Request เป็น Command/Query แล้วส่งเข้า Application Layer ผ่าน MediatR
- คืนผลลัพธ์จาก Application ให้ Client เช่น Frontend หรือ Mobile App

**ตัวอย่างที่อยู่ใน WebAPI:**
- `CustomerController`
- `OrderController`
- Swagger Config, Middleware, Validation Filters

**กฎสำคัญ:**
- ไม่ทำ Business Logic ใน Controller
- แค่รับข้อมูล, เรียก MediatR, ส่ง Response

---

# 🌟 สรุป Flow ของโปรเจกต์นี้

```plaintext
[ Client / Frontend ]
        ⬇️ HTTP
[ WebAPI (Controller) ]
        ⬇️ MediatR
[ Application Layer (Command/Query, Handler) ]
        ⬇️
[ Domain (Entity, Interface) ]
        ⬆️
[ Infrastructure (Repository, Dapper, SQL Server)]
```

- ✔️ Flow เดินทางเดียว ไม่มีวน Loop
- ✔️ แต่ละ Layer แยกความรับผิดชอบชัดเจน
- ✔️ ทดสอบและขยายได้ง่าย

---

# 🛠️ ประโยชน์ที่ได้จาก Clean Architecture

- ✨ ทดสอบ Business Logic ได้โดยไม่ต้องพึ่ง Database จริง
- ✨ เปลี่ยน Database จาก SQL Server ไปเป็น PostgreSQL, MongoDB ได้โดยไม่แก้ Application
- ✨ เปลี่ยน WebAPI เป็น GraphQL หรือ gRPC ได้โดยไม่กระทบ Domain
- ✨ รองรับทีมงานหลายคนพัฒนาแต่ละ Layer พร้อมกันได้อย่างปลอดภัย

---

# 💼 หมายเหตุเพิ่มเติม
- ควรทำ Validation ที่ Application หรือ Controller ก่อนส่งเข้า Domain
- หากโปรเจกต์โตขึ้น สามารถแยก `Infrastructure` ออกเป็นหลายๆ Project (เช่น `Infra.Data`, `Infra.ExternalServices`)

---

# 🚀 Let's build scalable systems together!

