import { useEffect } from "react";
import DeleteDepartmentModal from "../DeleteDepartmentModal";

const DeletePage = () => {
  useEffect(() => {
    function showModal() {
      const modal = document.getElementById(
        "delete_department_modal"
      ) as HTMLDialogElement;
      if (modal !== null) {
        modal.showModal();
      }
    }
    showModal();
  }, []);

  return (
    <div className="flex flex-col gap-4">
      <DeleteDepartmentModal />
    </div>
  );
};

export default DeletePage;
