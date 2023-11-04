import { ApolloClient, InMemoryCache, createHttpLink } from '@apollo/client';
import { setContext } from '@apollo/client/link/context';
import userEvent from '@testing-library/user-event';

const API_ENDPOINT = 'https://localhost:7133/graphql/';
const authLink = setContext((_, { headers }) => {
    const currentUser = JSON.parse(localStorage.getItem('user'));

    if (currentUser) {
        const token = currentUser.token;
        return {
            headers: {
                ...headers,
                Authorization: token ? `Bearer ${token}` : ''
            },
        };
    }
});

const httpLink = createHttpLink({
    uri: API_ENDPOINT,
});

const client = new ApolloClient({
    link: authLink.concat(httpLink),
    cache: new InMemoryCache(),
    context: {},
    onError: ({ networkError, graphQLErrors }) => {
        console.log('graphQLErrors', graphQLErrors)
        console.log('networkError', networkError)
    }
});

export default client;