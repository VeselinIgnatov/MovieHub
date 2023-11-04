import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import { registerUserMutation, loginUserMutation } from '../mutations/user-mutations';
import { RssFeed } from "@mui/icons-material";

export const registerUser = createAsyncThunk('user/registerUser', async (user) => {
    const response = await registerUserMutation(user);
    return response;
})

export const loginUser = createAsyncThunk('user/loginUser', async (credentials) => {

    const response = await loginUserMutation(credentials)
    return response;
})


const initialState = {
    user: {},
    loading: false,
    error: null
}

const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {
        setUser: (state, action) => {
            state.user = action.payload;
            state.loading = false;
            state.error = ''
        },
        logoutUser: (state) => {
            state.user = {}
        }
    },
    extraReducers: (builder) => {
        builder.addCase(registerUser.pending, (state, action) => {
            state.loading = true;
        })
        builder.addCase(registerUser.fulfilled, (state, action) => {
            state.user = action.payload
            state.loading = false;
        })
        builder.addCase(registerUser.rejected, (state, action) => {
            state.user = {}
            state.loading = false;
            state.error = "ERROR"
        })
        builder.addCase(loginUser.pending, (state, action) => {
            state.loading = true;
        })
        builder.addCase(loginUser.fulfilled, (state, action) => {
            state.user = action.payload
            state.user.isAuthenticated = true
            state.loading = false;
            state.error = ''
            localStorage.setItem('user', JSON.stringify(state.user))
        })
        builder.addCase(loginUser.rejected, (state, action) => {
            state.user = {}
            state.loading = false;
            state.error = "ERROR"
        })
    }
});

export const { setUser, logoutUser } = userSlice.actions;
export default userSlice.reducer;




