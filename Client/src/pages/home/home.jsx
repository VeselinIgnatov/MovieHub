import { CardList, Search, PageNavigation } from "../../components/"
import { getGenres } from "../../queries/show-query"
import { useEffect, useState } from "react"

const Home = () => {

    const [genres, setGenres] = useState([])
    useEffect(() => {
        async function getShowGenres() {

            const data = await getGenres()
            setGenres(data.distinctGenres)
        }

        getShowGenres()
    }, [])
    return (
        <div style={{ marginBottom: '20px' }}>
            <Search />
            <CardList />
            <PageNavigation />
        </div>
    )
}

export default Home;