import {
    Home,
    Authentication
  } from '../pages';
  
  export const nav = [
    {
      path: "/",
      name: "Home",
      element: <Home />,
      isMenu: false,
      isPrivate: true,
    },{
        path: "/login",
        name: "Login",
        element: <Authentication />,
        isMenu: false,
        isPrivate: false,
        isAnonymousOnly: true
      },{
        path: "/register",
        name: "Register",
        element: <Authentication />,
        isMenu: false,
        isPrivate: false,
        isAnonymousOnly: true
      }
  ];
  