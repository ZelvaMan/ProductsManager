module ProductsManager exposing (..)

-- Press buttons to increment and decrement a counter.
--
-- Read how it works:
--   https://guide.elm-lang.org/architecture/buttons.html
--


import Browser
import Html exposing (Html, button, div, text)
import Html.Events exposing (onClick)
import Bulma.Layout exposing (..)



-- MAIN


main =
  Browser.element { init = init, update = update, view = view }



-- MODEL


type alias Model = Int


init : Model
init =
  0

type Msg
  = LoadProducts     

-- UPDATE


update : Msg -> Model -> Model
update msg model =
  --dodelat
    model



-- VIEW


view : Model -> Html Msg
view model 
   = main_ []
    [
    
    
    ]