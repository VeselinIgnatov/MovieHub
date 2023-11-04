import { Link } from 'react-router-dom';
import { useSelector, useDispatch } from 'react-redux'
import { logoutUser } from '../../features/user-slice';
import './header.css'
import { reset } from '../../features/shows-slice';

const Header = () => {

    const { user } = useSelector((state) => state.user)
    const dispatch = useDispatch();

    const handleLogout = () => {
        dispatch(reset())
        dispatch(logoutUser())
        localStorage.removeItem("user");
        localStorage.removeItem("search");
    }
    return (
        <div className="header-container">
            <div className="upper-header">
                <div>
                    <Link to='/'><button className="home-button navbar-button" >Home</button></Link>
                </div>
                <div className="buttons">
                    {user.isAuthenticated
                        ? <button className="log-out-button navbar-button" onClick={handleLogout}>Logout</button>
                        : <>
                            <Link to='login'><button className="sign-in-button navbar-button">Sign in</button></Link>
                            <Link to='register'><button className="join-us-button navbar-button">Join us</button></Link>
                        </>
                    }
                </div>
            </div>
        </div>
    )
}
export default Header