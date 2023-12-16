tạo department -> tạo position(nhân viên)

tạo employee vào deparment

tạo điểm danh

tạo bảng lương


ở bên quản lý emp, tạo mới emp, vào trang department, xem danh sách position, chọn một position, bấm thêm nhân viên, chọn nhân viên chưa có position-> add nhân viên mới

luồng ở màn hình department
1/ Get list department
2/ chọn một department -> view list positions/ add new position
3/ chọn một position -> view list nhân viên / add new nhân viên(gọi api list non-position)
4/ chọn một nhân viên -> xóa nhân viên/ xóa position 

luồng chuyển nhân viên từ position này, department này chuyển sang phòng ban khác position khác
1/chọn nhân viên muốn chuyển
2/xóa position( vẫn giữ quan hệ với department đó)
3/ vào position của phòng ban mới, chọn Add new nhân viên (gọi api list non-position)