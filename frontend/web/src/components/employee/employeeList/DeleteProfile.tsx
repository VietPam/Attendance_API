const DeleteProfile = () => {
    return (
      <div>
        {" "}
        <dialog
          id="delete_profile_modal"
          className="modal modal-bottom sm:modal-middle"
        >
          <div className="modal-box">
            <h3 className="font-bold text-lg">Delete Employee Profile</h3>
            <div>
              <h3>Are you sure?</h3>
            </div>
            <div className="modal-action flex justify-center">
              <form method="dialog">
                {/* if there is a button in form, it will close the modal */}
                <button className="btn btn-sm btn-circle btn-ghost absolute right-2 top-2">
                  ✕
                </button>
              </form>
              <form method="dialog">
                {/* if there is a button in form, it will close the modal */}
                <div className="flex justify-center gap-1">
                  <button className="btn">No</button>
                  <button className="btn btn-error text-white hover:text-black">
                    Yes
                  </button>
                </div>
              </form>
            </div>
          </div>
        </dialog>
      </div>
    );
  };
  
  export default DeleteProfile;
  