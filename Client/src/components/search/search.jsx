import React, { useState, useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { setSearchValue, setSelectedGenre, getDistinctGenres } from '../../features/search-slice';
import { getShowsList } from '../../features/shows-slice';
import './search.css';

const Search = () => {

    const [search, setSearch] = useState({
        genre: '',
        name: ''
    });

    const dispatch = useDispatch();
    const searchState = useSelector((state) => state.search)
    const showsState = useSelector((state) => state.shows)

    useEffect(() => {
        const lsSearch = JSON.parse(localStorage.getItem("search"))
        if (lsSearch) {
            console.log(lsSearch)
            const searchValues = {
                genre: lsSearch.selectedGenre,
                name: lsSearch.searchValue
            }

            setSearch(searchValues)
            dispatch(getShowsList(searchValues))

        }
        dispatch(getDistinctGenres())
    }, [])


    const handleInput = (e) => {
        const { value } = e.target
        setSearch((values) => ({
            ...values,
            name: value
        }))
        dispatch(setSearchValue(value))
    }
    const handleSearch = () => {
        dispatch(getShowsList(search));
    }

    const handleGenreSelect = (e) => {
        const { value } = e.target;
        console.log('handleGenreSelect')
        const newSearch = {
            ...search,
            genre: value
        }
        setSearch(newSearch)
        dispatch(setSelectedGenre(value))
        dispatch(getShowsList(newSearch))
    }
    return (
        <div className='search-container'>
            <div className='search-area search-field'>
                <input placeholder="Enter product name" name='name' onInput={handleInput} value={search.name} />
                <button onClick={handleSearch}>Search</button>
            </div>
            <div className='search-area genres-select-list'>
                <select onChange={handleGenreSelect} name='genre'>
                    <option disabled>{'Choose genre..'}</option>
                    {searchState.genreList && searchState.genreList.map((genre) => {
                        return <option key={genre} value={genre} >{genre}</option>
                    })
                    }
                </select>
            </div>
        </div>)
}

export default Search