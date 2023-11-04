import React from 'react'
import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import { getShows } from '../queries/show-query';

const initialState = {
    shows: [],
    loading: false,
    error: null,
    take: 9,
    currentPage: 1,
    pagingInfo: {
        startCursor: '',
        endCursor: '',
        hasNextPage: false,
        hasPreviousPage: false,
    }
}

export const getShowsList = createAsyncThunk('shows/getShowsList', async (searchInput) => {
    const data = await getShows({ ...searchInput, slice: '' })
    return data
})

export const getShowsPage = createAsyncThunk('shows/getShowsPage', async (searchInput) => {

    const slice = searchInput.isForward ? `after: "${searchInput.endCursor}",` : `before: "${searchInput.startCursor}",`
    const data = await getShows({ slice, genre: searchInput.genre, name: searchInput.name })
    return data
})


const showSlice = createSlice({
    name: 'shows',
    initialState,
    reducers: {
        reset: (state) => {
            state.shows = []
            state.loading = false
            state.error = null
            state.take = 9
            state.currentPage = 1
            state.pagingInfo = {
                startCursor: '',
                endCursor: '',
                hasNextPage: false,
                hasPreviousPage: false
            }
        },
        updatePagingInfo: (state, action) => {
            state.currentPage = action.payload.currentPage
        },
    },
    extraReducers: (builder) => {
        builder.addCase(getShowsList.pending, (state, action) => {
            state.loading = true;
        })
        builder.addCase(getShowsList.fulfilled, (state, action) => {
            state.shows = action.payload.shows.nodes
            state.pagingInfo = action.payload.shows.pageInfo
            state.currentPage = 1

            state.loading = false;
        })
        builder.addCase(getShowsList.rejected, (state, action) => {
            state.shows = []
            state.loading = false;
            state.error = "ERROR"
        })
        builder.addCase(getShowsPage.pending, (state, action) => {
            state.loading = true;
        })
        builder.addCase(getShowsPage.fulfilled, (state, action) => {
            state.shows = action.payload.shows.nodes
            state.pagingInfo = action.payload.shows.pageInfo

            state.loading = false;
        })
        builder.addCase(getShowsPage.rejected, (state, action) => {
            state.shows = []
            state.loading = false;
            state.error = "ERROR"
        })
    }
});

export const { updatePagingInfo, reset } = showSlice.actions
export default showSlice.reducer;
