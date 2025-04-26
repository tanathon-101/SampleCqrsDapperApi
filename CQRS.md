# CQRS และ MediatR Overview

โปรเจกต์นี้ใช้หลักการ **CQRS** ร่วมกับ **MediatR** เพื่อแยกความรับผิดชอบของระบบ Read และ Write ออกจากกันอย่างชัดเจน และลด Coupling ระหว่างชั้น Controller กับชั้น Service

---

# ✨ CQRS (Command Query Responsibility Segregation)

## หลักการสำคัญ
- แยก **Command** (เขียน/เปลี่ยนแปลงข้อมูล) ออกจาก **Query** (อ่านข้อมูล)
- ทำให้โค้ดแต่ละตัวมีหน้าที่เฉพาะทางชัดเจน (Single Responsibility)

## ตัวอย่างการแยก
| ประเภท | Example | อธิบาย |
|:---|:---|:---|
| Command (Write) | `CreateCustomerCommand` | สร้างข้อมูล Customer ใหม่ |
| Query (Read) | `GetCustomersQuery` | อ่านข้อมูล Customer ทั้งหมด |

## ประโยชน์ของ CQRS
- ลดความซับซ้อนในโค้ด
- Scale ระบบ Read/Write แยกกันได้
- ง่ายต่อการทำ Audit, Caching, Event sourcing ในอนาคต

---

# 🛠 MediatR (Mediator Pattern)

## หน้าที่หลัก
- เป็นตัวกลางส่งข้อมูลจาก Controller ไปหา Handler ที่เหมาะสม
- ช่วยให้ Controller ไม่ต้องรู้จัก Service หรือ Repository ตรง ๆ

## Flow การทำงาน
```plaintext
[ Controller ]
    ↓ ส่ง Command/Query ผ่าน MediatR
[ MediatR ]
    ↓ จับคู่ Handler ที่ตรงกับ Command/Query
[ Handler ]
    ↓ ทำงานแล้วส่งผลลัพธ์กลับไป
```

## ตัวอย่างการทำงานจริง
- Controller รับ Request ➔ สร้าง `CreateCustomerCommand` ➔ ส่งให้ MediatR ➔ MediatR หาคนรับ (Handler) ➔ Handler สั่ง Repository ทำงาน ➔ คืน Result

---

# 📦 Libraries ที่ใช้งาน

- [MediatR](https://github.com/jbogard/MediatR) — Core library สำหรับ MediatR Pattern
- [MediatR.Extensions.Microsoft.DependencyInjection](https://www.nuget.org/packages/MediatR.Extensions.Microsoft.DependencyInjection) — สำหรับ Integration กับ ASP.NET Core DI System

---

# 🖼️ รูปแบบ Diagram CQRS + MediatR

```plaintext
[ Client / Frontend ]
        ↓ HTTP
[ WebAPI (Controller) ]
        ↓ MediatR
[ Application (Command/Query, Handler) ]
        ↓
[ Domain (Entity, Interface) ]
        ↑
[ Infrastructure (Repository, Database)]
```

- Command ➔ Handler ➔ Entity ➔ Database
- Query ➔ Handler ➔ Database ➔ Return Result

---

# 🛠 ตัวอย่าง Command และ Handler (ย่อ)

## CreateCustomerCommand.cs
```csharp
public class CreateCustomerCommand : IRequest<int>
{
    public string Name { get; set; }
    public string Email { get; set; }
}
```

## CreateCustomerCommandHandler.cs
```csharp
public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
{
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Customer { Name = request.Name, Email = request.Email };
        return await _customerRepository.CreateCustomerAsync(customer);
    }
}
```

---

# 🚀 สรุป

| หัวข้อ | ประโยชน์ |
|:---|:---|
| CQRS | ทำให้ Read/Write แยกกันชัดเจน, รองรับ Scaling ได้ดี |
| MediatR | ลด Coupling, ทำให้ Controller เรียก Handler ผ่านตัวกลางอย่างเป็นระบบ |

**ทำให้ระบบยืดหยุ่น, ทดสอบง่าย, และพร้อมขยายในอนาคต!** 🚀

