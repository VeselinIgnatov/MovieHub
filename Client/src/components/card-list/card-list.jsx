import React from 'react'
import { ShowCard, Spinner } from '../../components'
import { useSelector } from 'react-redux'
import './card-list.css'


const CardList = () => {

    const showsState = useSelector((state) => state.shows);
    const loading = showsState.loading;
    const shows = showsState.shows;
    console.log(shows)
    return (
        <div className='card-list'>
            {loading ? <Spinner />
                : shows.length !== 0 ?
                    <ul>
                        {shows.map((show) => {
                            return <li key={show.id}><ShowCard show={show}></ShowCard></li>
                        })
                        }
                    </ul>
                    : <div className="no-products-found">
                        <span>NO SHOWS FOUND</span>
                    </div>
            }
        </div>
    )
}


export default CardList