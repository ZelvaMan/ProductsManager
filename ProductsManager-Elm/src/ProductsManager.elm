module ProductsManager exposing (..)

-- Press buttons to increment and decrement a counter.
--
-- Read how it works:
--   https://guide.elm-lang.org/architecture/buttons.html
--

import Browser
import Bulma.Layout exposing (..)
import Html exposing (..)
import Html.Events exposing (onClick)
import Http
import Json.Decode as Decode



-- MAIN


main =
    Browser.element
        { init = init
        , update = update
        , view = view
        }



--


type Availability
    = InCart
    | Avalible
    | UnAvailable


type alias Product =
    { id : Int
    , name : String
    , price : Int
    , inStock : Int
    , avail : Availability
    }



-- MODEL


apiurl =
    "localhost:51786/api"


type alias Model =
    Int


init : Model
init =
    0


type Msg
    = LoadProducts
    | ProductsLoaded (Result Http.Error (List Product))



-- UPDATE


update : Msg -> Model -> ( Model, Cmd Msg )
update msg model =
    --dodelat
    case msg of
        LoadProducts ->
            handleLoadProducts model



-- VIEW


handleLoadProduct : Model -> ( Model, Cmd Msg )
handleLoadProduct model =
    ( model, LoadProducts )


loadProducts : Cmd Msg
loadProducts =
    Http.get
        { url = apiurl + "/Products"
        , expect = Http.exeptJson ProductsLoaded getAllProductsResponseDecoder
        }


getAllProductsResponseDecoder : Decode.Decoder (List Product)
getAllProductsResponseDecoder =
    Decode.list Product productDecoder


productDecoder : Decode.Decoder Product
productDecoder =
    decode Product
        |> required "id" Decode.int
        |> required "name" Decode.string
        |> required "price" Decode.int
        |> required "inStock" Decode.int
        |> required "productAvailability" availabilityDecoder


availabilityDecoder : Decode.Decoder Int -> Decode.Decoder Availability
availabilityDecoder =
    Decode.andThen
        (\x ->
            (case x of
                0 ->
                    InCart

                1 ->
                    Avalible

                2 ->
                    UnAvailable
            )
                |> decode.succeed
        )


view : Model -> Html Msg
view model =
    div []
        []
