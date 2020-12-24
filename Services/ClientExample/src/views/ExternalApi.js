import React, {useState} from "react";
import {Alert, Button} from "reactstrap";
import Highlight from "../components/Highlight";
import {useAuth0, withAuthenticationRequired} from "@auth0/auth0-react";
import config from "../auth_config.json";
import Loading from "../components/Loading";
import axios from 'axios';

const {apiOrigin = "https://localhost:5001"} = config;

export const ExternalApiComponent = () => {
    const [state, setState] = useState({
        showResult: false,
        apiMessage: "",
        error: null,
    });

    const {
        getAccessTokenSilently,
        loginWithPopup,
        getAccessTokenWithPopup,
    } = useAuth0();

    const handleConsent = async () => {
        try {
            await getAccessTokenWithPopup();
            setState({
                ...state,
                error: null,
            });
        } catch (error) {
            setState({
                ...state,
                error: error.error,
            });
        }

        await callApi();
    };

    const handleLoginAgain = async () => {
        try {
            await loginWithPopup();
            setState({
                ...state,
                error: null,
            });
        } catch (error) {
            setState({
                ...state,
                error: error.error,
            });
        }

        await callApi();
    };

    const callApi = async () => {
        try {
            const token = await getAccessTokenSilently();

            axios.get(
                `${apiOrigin}/api/accounts/2120e9d8-6c32-46b8-8e78-8bf3d14dcd4e/`,
                {                    headers: {
                        Authorization: `Bearer ${token}`,
                    }}
            )
                .then(res => {
                    setState({
                        ...state,
                        showResult: true,
                        apiMessage: res,
                    });
                })

        } catch (error) {
            setState({
                ...state,
                error: error.error,
            });
        }
    };

    const handle = (e, fn) => {
        e.preventDefault();
        fn();
    };

    return (
        <>
            <div className="mb-5">
                {state.error === "consent_required" && (
                    <Alert color="warning">
                        You need to{" "}
                        <a
                            href="#/"
                            class="alert-link"
                            onClick={(e) => handle(e, handleConsent)}
                        >
                            consent to get access to users api
                        </a>
                    </Alert>
                )}

                {state.error === "login_required" && (
                    <Alert color="warning">
                        You need to{" "}
                        <a
                            href="#/"
                            class="alert-link"
                            onClick={(e) => handle(e, handleLoginAgain)}
                        >
                            log in again
                        </a>
                    </Alert>
                )}

                <h1>External API</h1>
                <p>
                    Ping an external API by clicking the button below. This will call the
                    external API using an access token, and the API will validate it using
                    the API's audience value.
                </p>

                <Button color="primary" className="mt-5" onClick={callApi}>
                    Ping API
                </Button>
            </div>

            <div className="result-block-container">
                {state.showResult && (
                    <div className="result-block" data-testid="api-result">
                        <h6 className="muted">Result</h6>
                        <Highlight>
                            <span>{JSON.stringify(state.apiMessage, null, 2)}</span>
                        </Highlight>
                    </div>
                )}
            </div>
        </>
    );
};

export default withAuthenticationRequired(ExternalApiComponent, {
    onRedirecting: () => <Loading/>,
});
