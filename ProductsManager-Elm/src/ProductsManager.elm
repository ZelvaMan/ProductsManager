module ProductsManager exposing (..)

import Browser
import Bulma.CDN exposing (..)
import Bulma.Columns exposing (..)
import Bulma.Components exposing (..)
import Bulma.Elements exposing (..)
import Bulma.Form exposing (..)
import Bulma.Layout exposing (..)
import Bulma.Modifiers
    exposing
        ( Color(..)
        , HorizontalAlignment(..)
        , Size(..)
        , State(..)
        , VerticalDirection(..)
        )
import Html exposing (text, Html, span)
import Html.Events exposing (onClick)
import Http exposing (..)
import Json.Decode as Decode exposing (..)
import Json.Decode.Pipeline exposing (..)



-- MAIN


main =
    Browser.element
        {  init = init
        , subscriptions = subs
        , update = update
        , view = view
        }

init : () -> (Model, Cmd msg)
init m =
 ( (Model Nothing   Products ), Cmd.none )
subs : Model -> Sub msg
subs model =
    Sub.none



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
    { products : Maybe (List Product)
    , page : Page
    }


type Msg
    = LoadProducts
    | ProductsLoaded (Result Http.Error (List Product))
    | ChangePage Page


type Page
    = Products
    | ProductDetail
    | Cart
    | OrderHistory
    | OrderDetail



-- UPDATE


update : Msg -> Model -> ( Model, Cmd Msg )
update msg model =
    --dodelat
    case msg of
        LoadProducts ->
            handleLoadProduct model

        ChangePage page_ ->
            ( { model | page = page_ }, Cmd.none )

        ProductsLoaded lproducts ->
            ( { model
                | products =
                    case lproducts of
                        Ok products ->
                            Just products

                        Err error ->
                            Nothing
              }
            , Cmd.none
            )



-- VIEW


handleLoadProduct : Model -> ( Model, Cmd Msg )
handleLoadProduct model =
    ( model, loadProducts )


loadProducts : Cmd Msg
loadProducts =
    Http.get
        { url = apiurl ++ "/Products"
        , expect = Http.expectJson ProductsLoaded getAllProductsResponseDecoder
        }


getAllProductsResponseDecoder : Decode.Decoder (List Product)
getAllProductsResponseDecoder =
    Decode.list productDecoder


productDecoder : Decode.Decoder Product
productDecoder =
    Decode.succeed Product
        |> required "id" Decode.int
        |> required "name" Decode.string
        |> required "price" Decode.int
        |> required "inStock" Decode.int
        |> required "productAvailability" (availabilityDecoder Decode.int)


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

                _ ->
                    UnAvailable
            )
                |> Decode.succeed
        )


view : Model -> Html Msg
view model =
    myHero model


myHero :Model-> Html Msg
myHero model  =
    easyHero
        (HeroModifiers
            False
            Large
            Default
        )
        []
        { head =
            heroHead []
                [ myNavbar True True ]
        , body =
            heroBody []
                [listOfProducts (model.products)]
        , foot =
            heroFoot []
                []
        }


myNavbar : Bool -> Bool -> Html Msg
myNavbar isMenuOpen isMenuDropdownOpen =
    navbar navbarModifiers
        []
        [ navbarBrand []
            ( navbarBurger False []
        [ span [] []
        , span [] []
        , span [] []
        ]
      )
            [ navbarItem False
                []
                [ easyImage Natural
                    []
                    "img\\elm_logo.png"
                ]
            ]
        , navbarMenu isMenuOpen
            []
            [ navbarStart []
                [ navbarItemLink False [] [ text "Home" ]
                , navbarItemLink False [] [ text "Blog" ]
                , navbarItemLink True [] [ text "Carrots" ]
                , navbarItemLink False [] [ text "About" ]
                ]
            , navbarEnd []
                [ navbarItemDropdown isMenuDropdownOpen
                    Down
                    []
                    (navbarLink
                        []
                        [ text "Menu"
                        ]
                    )
                    [ navbarDropdown False
                        Left
                        []
                        [ navbarItemLink False
                            []
                            [ easyButton buttonModifiers
                                []
                                (ChangePage Products)
                                "Products"
                            ]
                        , navbarItemLink False
                            []
                            [ easyButton buttonModifiers
                                []
                                (ChangePage Cart)
                                "Cart"
                            ]
                        , navbarItemLink True
                            []
                            [ easyButton buttonModifiers
                                []
                                (ChangePage OrderHistory)
                                "Order history"
                            ]
                        ]
                    ]
                ]
            ]
        ]

listOfProducts: (Maybe(List Product)) -> Html Msg
listOfProducts products 
     = table myTableModifiers []
    [ tableHead [] []
    , tableBody [] 
      (Maybe.withDefault (List.singleton( tableRow False  []
      [  
         tableCell [] [text "No products"]
      ]) ) 
      ( products |> Maybe.map (List.map productTableRow)))
    
    , tableFoot [] []
    ]
myTableModifiers : TableModifiers
myTableModifiers =
    { bordered  = True
    , striped =  True
    , narrow = True
    , hoverable = True
    , fullWidth = True
    }

productTableRow: Product -> TableRow msg
productTableRow product  =
    tableRow False  []
      [  
         tableCell [] [text (String.fromInt product.id)]
        , tableCell [] []
        , tableCell [] []
        , tableCell [] []
        , tableCell [] []
        , tableCell [] []
      ]

productsTableHead : Html Msg
productsTableHead 
  = tableHead []
    [ tableRow False []
      [ tableCellHead [] [text  "Id"]
      , tableCellHead [] [text  "Name"]
      , tableCellHead [] [text  "Price"]
      , tableCellHead [] [text  "In Stock"]
      , tableCellHead [] [text  "Avallibity"]
      , tableCellHead [] [text  "Add to cart"]
      ]
    ]