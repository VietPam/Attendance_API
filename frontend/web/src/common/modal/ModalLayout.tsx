import { MODAL_BODY_TYPES } from "../../utils/globalConstantUtil";
import { useSelector } from "react-redux";
import { RootState } from "../../store/store";
import AddDepartModalBody from "./addModal/AddDepartModalBody";
import ConfirmationModalBody from "./ConfirmationModalBody";

function ModalLayout() {
  const { isOpen, bodyType, size, extraObject, title } = useSelector(
    (state: RootState) => state.modal
  );

  return (
    <>
      {/* The button to open modal */}

      {/* Put this part before </body> tag */}
      <dialog
        id="modal_layout"
        className="modal modal-bottom sm:modal-middle"
        open={isOpen}
      >
        <div className={`modal-box ${size ? "lg" : "max-w-3xl"}`}>
          <h3 className="font-bold text-lg">{title}</h3>
          {bodyType === MODAL_BODY_TYPES.DEPARTMENT_ADD_NEW && (
            <AddDepartModalBody />
          )}

          {bodyType === MODAL_BODY_TYPES.CONFIRMATION && (
            <ConfirmationModalBody extraObject={extraObject} />
          )}
        </div>
      </dialog>
    </>
  );
}

export default ModalLayout;
