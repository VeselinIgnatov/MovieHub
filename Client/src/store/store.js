import { configureStore } from "@reduxjs/toolkit";
import showsReducer from '../features/shows-slice'
import userReducer from '../features/user-slice'
import searchReducer from '../features/search-slice'
import { combineReducers } from 'redux';

const reducer = combineReducers({
    shows: showsReducer,
    user: userReducer,
    search: searchReducer
})
const store = configureStore({
    reducer
});

export default store;

