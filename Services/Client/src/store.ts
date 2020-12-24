import {applyMiddleware, createStore} from 'redux';
import {composeWithDevTools} from 'redux-devtools-extension';
import {responsiveStoreEnhancer} from 'redux-responsive';
import createSagaMiddleware from 'redux-saga';
import rootReducer from './reducers';

export default function configureStore(routerMiddleware) {

    const sagaMiddleware = createSagaMiddleware()
    return {
        ...createStore(
            rootReducer,
            composeWithDevTools(
                responsiveStoreEnhancer,
                applyMiddleware(sagaMiddleware, routerMiddleware),
            ),
        ),
        runSaga: sagaMiddleware.run
    }
}
