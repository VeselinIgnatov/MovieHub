import React from 'react'
import { CircularProgress } from '@mui/material'
import './spinner.css'

const Spinner = () => {
    return (
        <div className='whole-page-spinner'>
            <CircularProgress />
        </div>
    )
}

export default Spinner