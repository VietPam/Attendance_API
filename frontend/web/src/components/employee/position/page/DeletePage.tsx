import { useEffect } from "react";
import DeletePositionModal from "../DeletePositionModal";

const DeletePage = () => {
  useEffect(() => {
    function showModal(type: string) {
      const modal = document.getElementById(type) as HTMLDialogElement;
      if (modal !== null) {
        modal.showModal();
      }
    }
    showModal("delete_position_modal");
  }, []);

  return (
    <div className="">
      <DeletePositionModal />
    </div>
  );
};

export default DeletePage;
