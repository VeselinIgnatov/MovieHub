import { gql } from "@apollo/client";
import client from "../client/apollo-client";

const getGenres = async (userInput) => {
  const GET_DISTINCT_GENRES = gql`query distinctGenres{
    distinctGenres
  }`

  var { data } = await client.mutate({
    mutation: GET_DISTINCT_GENRES,
  });

  return data
}

const getShows = async (searchInput) => {
  const GET_SHOWS = gql`query shows{
        shows(first: 9, ${searchInput?.slice} genre: "${searchInput?.genre}", name: "${searchInput?.name}"){
            nodes {
                id
                name
                premiered
                image {
                  medium
                }
                rating {
                  average
                }
                genres
                summary
              }
              pageInfo {
                startCursor
                endCursor
                hasNextPage
                hasPreviousPage
              }
        }
    }`

  var { data } = await client.mutate({
    mutation: GET_SHOWS,
  });

  return data
}

export {
  getGenres,
  getShows
}