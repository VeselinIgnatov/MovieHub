import { Route, Routes, Navigate } from "react-router-dom";
import { AuthData } from '../../auth/auth-wrapper'
import { nav } from '../../core/navigation'
import { useSelector } from "react-redux/es/hooks/useSelector";

const RenderRoutes = () => {
    const userState = useSelector((state) => state.user);    
    return (
        <Routes>
            {nav.map((r, i) => {
                if (r.isPrivate) {
                    return <Route key={i} path={r.path} element={userState.user.isAuthenticated ? r.element : <Navigate to='/login'/>} />;
                } else if (!r.isPrivate && !r.isAnonymousOnly) {
                    return <Route key={i} path={r.path} element={r.element} />;
                } else if (r.isAnonymousOnly) {
                    return <Route key={i} path={r.path} element={!userState.user.isAuthenticated ? r.element : <Navigate to='/'/>} />;
                }
            })}
        </Routes>
    );
};

export default RenderRoutes