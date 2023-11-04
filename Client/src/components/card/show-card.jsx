import React from 'react'
import {
    Card,
    CardMedia,
    CardContent,
    Typography,
} from '@mui/material';
import StarOutlineIcon from '@mui/icons-material/StarOutline';
import FavoriteBorderOutlinedIcon from '@mui/icons-material/FavoriteBorderOutlined';
import { IconButton } from '@mui/material';
import './show-card.css'

const ShowCard = ({ show }) => {

    const summaryMarkup = { __html: show.summary };

    return (
        <Card variant='outlined' sx={{ width: 350, borderRadius: 3 }}>
            <CardContent className='card-content content-top'>
                <div className='product-info'>
                    <Typography className='product-name' color='black' sx={{
                        height: '25px',
                        overflow: 'hidden',
                        textOverflow: 'ellipsis',
                        whiteSpace: 'no-wrap',
                        width: '222px'
                    }}>
                        {show.name}
                    </Typography>
                    <Typography sx={{ fontFamily: 'unset' }} className='product-brand' color='grey'>
                        {show.genres.join(', ')}
                    </Typography>
                </div>
                <IconButton className='fav-icon' >
                    <FavoriteBorderOutlinedIcon sx={{ verticalAlign: 'sub', color: 'rgb(190, 5, 5)' }} />
                </IconButton>
            </CardContent>
            <CardMedia
                component="img"
                height="194"
                image={show.image.medium}
                alt="Paella dish"
            />
            <CardContent className='card-content content-bottom'>
                <Typography className='product-description' color='black'>
                    {'Released: ' + show.premiered.slice(0, 4)}
                </Typography>
                <Typography className='product-rating' color='grey'>
                    <StarOutlineIcon sx={{ verticalAlign: 'sub', color: 'yellow' }} />
                    {Number(show.rating.average).toFixed(2)}
                </Typography>
            </CardContent>
            <span className='summary-section' dangerouslySetInnerHTML={summaryMarkup} />
        </Card>
    )
}

export default ShowCard