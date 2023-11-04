import { useEffect } from "react";
import { RenderRoutes, Header } from '../components';
import { setUser } from "../features/user-slice";
import { useDispatch, useSelector } from "react-redux";

export const AuthWrapper = () => {

    const dispatch = useDispatch();
    const userState = useSelector((state) => state.user)

    useEffect(() => {
        const lsUser = JSON.parse(localStorage.getItem('user'))

        if (lsUser && Object.keys(userState.user).length === 0) {
            dispatch(setUser(lsUser));
        }
    }, [userState.user])

    return (
        <>
            <Header />
            <RenderRoutes />
        </>
    );
};
