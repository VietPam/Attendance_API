import { useEffect } from "react";
import ChangeDepartmentModal from "../ChangeDepartmentModal";

const ChangePage = () => {
  useEffect(() => {
    function showModal() {
      const modal = document.getElementById(
        "change_department_modal"
      ) as HTMLDialogElement;
      if (modal !== null) {
        modal.showModal();
      }
    }
    showModal();
  }, []);

  return (
    <div className="flex flex-col gap-4">
      <ChangeDepartmentModal />
    </div>
  );
};

export default ChangePage;
