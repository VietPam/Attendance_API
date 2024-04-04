// import { Link } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { RootState } from "../store/store";

const Navbar = () => {
  const dispatch = useDispatch();
  const currentUser = useSelector((state: RootState) => state.auth);
  function handleLogout(): void {
    dispatch({ type: "LOGOUT" });
  }


  return (
    <>
      <div className="navbar py-4">
        <section className="md:flex-1 max-md:hidden">
          {/* <input
            type="text"
            placeholder="Search"
            className="input input-bordered w-24 md:w-auto"
          /> */}
          {/* <SearchBar /> */}
        </section>
        <section className="flex-none gap-2">
          <div className="flex items-center">
            {/* <div className="">
              <AvatarGroup />
            </div> */}
            {/* <div className="">
              <DropdownComponent />
            </div> */}
          </div>
          <div className="text-center">
            <p>{currentUser.email}</p>
            <p className="text-sm text-gray-500">Admin</p>
          </div>
          <div className="dropdown dropdown-end">
            <label tabIndex={0} className="btn btn-ghost btn-circle avatar">
              <div className="w-12 h-12 rounded-full">
                <img src="/woman (1).png" />
              </div>
            </label>
            <ul
              tabIndex={0}
              className="mt-3 z-[1] p-2 shadow menu dropdown-content bg-base-100 rounded-box w-52"
            >
              {/* <li>
                <Link to={"/adminprofile"}>Profile</Link>
              </li>
              <li>
                <Link to={"/changepassword"}>Password</Link>
              </li> */}
              <li>
                <a onClick={handleLogout}>Logout</a>
              </li>
            </ul>
          </div>
        </section>
      </div>
    </>
  );
};

export default Navbar;
