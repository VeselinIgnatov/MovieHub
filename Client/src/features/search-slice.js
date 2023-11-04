import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import { getGenres } from '../queries/show-query';

export const getDistinctGenres = createAsyncThunk('search/getDistinctGenres', async () => {
    const data = await getGenres()
    return data;
})

const initialState = {
    search: {
        selectedGenre: '',
        searchValue: ''
    },
    genreList: [],
    loading: false,
    error: null
}

const searchSlice = createSlice({
    name: 'search',
    initialState,
    reducers: {
        reset: (state) => {
            state = initialState
            localStorage.removeItem("search")
        },
        setSearchValue: (state, action) => {
            state.search.searchValue = action.payload
            localStorage.setItem("search", JSON.stringify(state.search))
        },
        setSelectedGenre: (state, action) => {
            state.search.selectedGenre = action.payload
            localStorage.setItem("search", JSON.stringify(state.search))
        },
    },
    extraReducers: (builder) => {
        builder.addCase(getDistinctGenres.pending, (state) => {
            console.log('pending', state)
            state.loading = true;
        })
        builder.addCase(getDistinctGenres.fulfilled, (state, action) => {
            console.log('payload', state)
            state.genreList = action.payload.distinctGenres
            state.loading = false;
        })
        builder.addCase(getDistinctGenres.rejected, (state) => {
            console.log('rejected', state)
            state = initialState
            state.loading = false
            state.error = "ERROR"
        })
    }
});

export const { reset, setSearchValue, setSelectedGenre } = searchSlice.actions;
export default searchSlice.reducer;




