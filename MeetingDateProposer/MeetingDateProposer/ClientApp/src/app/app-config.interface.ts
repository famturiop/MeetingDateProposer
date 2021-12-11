export interface IAppConfig {

    backEndpoint: string;

    googleOAuthSettings: {
        authEndpoint: string,
        accessType: string,
        responseType: string,
        clientId: string,
        redirectUri: string,
        scope: string,
        flowName: string
    }
}