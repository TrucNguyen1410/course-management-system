# 🎓 Course Management System

[![OOAD](https://img.shields.io/badge/Methodology-OOAD-0052CC?style=flat-square)](#)
[![UML](https://img.shields.io/badge/Design-UML-FF8C00?style=flat-square)](#)
[![Database](https://img.shields.io/badge/Database-Relational_DB-4EA94B?style=flat-square)](#)

**Course Management System** (Hệ thống Quản lý Giáo dục / Khóa học) là dự án áp dụng sâu sắc các nguyên lý **Phân tích và Thiết kế Hướng đối tượng (OOAD)**. Hệ thống cung cấp giải pháp toàn diện để quản lý luồng đào tạo, hỗ trợ tương tác đa phân quyền giữa Quản trị viên, Giảng viên và Sinh viên.

---

## 📸 Giao diện & Biểu đồ thiết kế
* **Màn hình của Admin:**
<img width="1866" height="908" alt="image" src="https://github.com/user-attachments/assets/563dbb56-8cc4-42bc-b471-381084e2e7a4" />
<img width="1861" height="897" alt="image" src="https://github.com/user-attachments/assets/27875418-9a5f-469b-a0f4-782762efefb7" />
<img width="1888" height="887" alt="image" src="https://github.com/user-attachments/assets/6bc893ed-5ff5-48ef-9db6-575f2f2561c0" />
<img width="1881" height="901" alt="image" src="https://github.com/user-attachments/assets/d4d2457e-f581-4957-a2e8-86c39fdadfac" />
<img width="1881" height="902" alt="image" src="https://github.com/user-attachments/assets/b4e2319a-3f38-4203-98c3-868b9f8ddd33" />
<img width="1876" height="901" alt="image" src="https://github.com/user-attachments/assets/e0e52077-6ab3-460e-8eb4-4883958dc8b0" />


---

* **Màn hình của Người dùng:**
<img width="1874" height="902" alt="image" src="https://github.com/user-attachments/assets/02c7f1f0-4b9f-49d5-b26d-3e8c277906e3" />
<img width="1885" height="899" alt="image" src="https://github.com/user-attachments/assets/6b9c9b34-2e22-46cf-a1da-4d0d209ec0e0" />
<img width="1877" height="894" alt="image" src="https://github.com/user-attachments/assets/d08cc548-2d21-470c-8f6c-007fec245eee" />
<img width="1863" height="903" alt="image" src="https://github.com/user-attachments/assets/46c3bc97-4cc4-4383-9e74-c22399fc82b8" />
<img width="1866" height="895" alt="image" src="https://github.com/user-attachments/assets/89c0b754-b140-4ae6-8317-8505371980d2" />
<img width="1863" height="898" alt="image" src="https://github.com/user-attachments/assets/0274c072-ae28-47d7-83f6-fb33aac37935" />


---

## ✨ Chức năng & Phân quyền hệ thống

Hệ thống được thiết kế với 3 luồng tác nhân (Actor) chính:

### 👨‍🎓 Sinh viên (Student)
- **Đăng ký học phần:** Xem danh sách môn học mở và đăng ký lớp theo học kỳ.
- **Theo dõi lộ trình:** Xem chương trình đào tạo, tiến độ học tập và thời khóa biểu cá nhân.
- **Tra cứu điểm số:** Xem điểm thành phần, điểm tổng kết và tự động tính toán điểm trung bình tích lũy (GPA).

### 👩‍🏫 Giảng viên (Teacher/Instructor)
- **Quản lý lớp học:** Xem danh sách sinh viên, điểm danh và theo dõi tình trạng lớp.
- **Cập nhật điểm số:** Nhập và quản lý điểm thi, điểm quá trình cho từng sinh viên trong lớp phụ trách.

### 🛠 Quản trị viên (Admin)
- **Quản lý hồ sơ:** Thêm, sửa, xóa thông tin hồ sơ của Giảng viên và Sinh viên.
- **Quản lý đào tạo:** Thiết lập môn học mới, mở lớp học phần và sắp xếp giảng viên giảng dạy.
- **Thống kê & Báo cáo:** Xuất dữ liệu thống kê về tình trạng đăng ký, chất lượng đào tạo của khoa/trường.

---

## 🛠 Phương pháp & Công nghệ

Dự án này tập trung mạnh vào quy trình chuẩn của Kỹ thuật Phần mềm:

- **Phương pháp luận:** Object-Oriented Analysis and Design (OOAD).
- **Mô hình hóa (UML):** - Use Case Diagram (Đặc tả yêu cầu).
  - Class Diagram (Thiết kế cấu trúc dữ liệu và đối tượng).
  - Sequence / Activity Diagram (Mô tả luồng nghiệp vụ).
- **Cơ sở dữ liệu:** Thiết kế cơ sở dữ liệu quan hệ (Relational Database / ERD).
- **Ngôn ngữ / Nền tảng:** `(Bạn điền ngôn ngữ sử dụng như C#, Java hoặc Python vào đây nhé)`

---

## ⚙️ Hướng dẫn Cài đặt

1. **Clone repository:**
   ```bash
   git clone [https://github.com/TrucNguyen1410/course-management-system.git](https://github.com/TrucNguyen1410/course-management-system.git)
