import { useState } from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { updatePagingInfo, getShowsPage } from '../../features/shows-slice'
import './page-navigation.css'

const PageNavigation = () => {

    const dispatch = useDispatch()
    const showsState = useSelector((state) => state.shows)
    const searchState = useSelector((state) => state.search)

    const handleShowNextPage = () => {

        if (!showsState.pagingInfo.hasNextPage) return;

        const data = {
            currentPage: showsState.currentPage + 1
        }

        const options = getPagingOptions(true);
        dispatch(updatePagingInfo(data))
        dispatch(getShowsPage(options))
    }

    const handleShowPrevPage = () => {

        if (!showsState.pagingInfo.hasPreviousPage) return;

        const data = {
            currentPage: showsState.currentPage - 1
        }

        const options = getPagingOptions(false);

        dispatch(updatePagingInfo(data))
        dispatch(getShowsPage(options))
    }

    const getPagingOptions = (isForward) => {
        return {
            genre: searchState.search.selectedGenre,
            name: searchState.search.searchValue,
            isForward: isForward,
            startCursor: showsState.pagingInfo.startCursor,
            endCursor: showsState.pagingInfo.endCursor
        }
    }

    return (
        <nav>
            {showsState.shows.length !== 0 ?
                (<><button
                    className='pagination-button prev-page'
                    disabled={!showsState.pagingInfo.hasPreviousPage}
                    onClick={handleShowPrevPage}
                >
                    <b>{"<"}</b>
                </button>
                    <div className='pagination-button page-count'><b>{showsState.currentPage}</b></div>
                    <button
                        className='pagination-button next-page'
                        disabled={!showsState.pagingInfo.hasNextPage}
                        onClick={handleShowNextPage}>
                        <b>{">"}</b>
                    </button></>)
                : null
            }
        </nav>
    )
}

export default PageNavigation