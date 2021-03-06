/ * = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =    
   *   C o p y r i g h t   ( c )   2 0 1 5   Q u a l c o m m   C o n n e c t e d   E x p e r i e n c e s ,   I n c .   A l l   R i g h t s   R e s e r v e d .    
   *   A n d   T h e S a v a g e R u ' s  
   *   = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = * /  
 u s i n g   U n i t y E n g i n e ;  
 u s i n g   U n i t y E n g i n e . U I ;  
 u s i n g   S y s t e m . C o l l e c t i o n s ;  
 u s i n g   S y s t e m . C o l l e c t i o n s . G e n e r i c ;  
 u s i n g   S y s t e m . L i n q ;  
 u s i n g   V u f o r i a ;  
 u s i n g   S y s t e m ;  
 u s i n g   S y s t e m . I O ;  
 u s i n g   A s s e m b l y C S h a r p ;  
 u s i n g   U n i t y E n g i n e . S c e n e M a n a g e m e n t ;  
  
  
 p u b l i c   c l a s s   U D T E v e n t H a n d l e r   :   M o n o B e h a v i o u r ,   I U s e r D e f i n e d T a r g e t E v e n t H a n d l e r  
 {  
         # r e g i o n   P U B L I C _ M E M B E R S  
         / / /   < s u m m a r y >  
         / / /   C a n   b e   s e t   i n   t h e   U n i t y   i n s p e c t o r   t o   r e f e r e n c e   a   I m a g e T a r g e t B e h a v i o u r   t h a t   i s   i n s t a n c i a t e d   f o r   a u g m e n t a t i o n s   o f   n e w   u s e r   d e f i n e d   t a r g e t s .  
         / / /   < / s u m m a r y >  
         p u b l i c   I m a g e T a r g e t B e h a v i o u r   I m a g e T a r g e t T e m p l a t e ;  
         p u b l i c   G a m e O b j e c t   t a k e P h o t o ;  
 	 p u b l i c   G a m e O b j e c t   b a c k B t n ;  
         p u b l i c   G a m e O b j e c t   v i e w G r a p h s ;  
         p u b l i c   G a m e O b j e c t   e d i t G r a p h ;  
         p u b l i c   G a m e O b j e c t   s h a r e G r a p h ;  
 	 p u b l i c   G a m e O b j e c t   f o r m ;  
 	 p u b l i c   G a m e O b j e c t   U I ;  
 	 p u b l i c   G a m e O b j e c t   l o a d S c r e e n ;  
 	 p u b l i c   G a m e O b j e c t   f r a m e C a n v a s ;  
  
 	 p r i v a t e   b o o l   i s P r o c e s s i n g   =   f a l s e ;  
 	 p u b l i c   s t r i n g   m e s s a g e   =   " S h a r e d   m u t h a f u c k e r " ;  
  
         p r o t e c t e d   b o o l   e d i t C l i c k e d   =   f a l s e ;  
         p u b l i c   i n t   L a s t T a r g e t I n d e x  
         {  
                 g e t   {   r e t u r n   ( m T a r g e t C o u n t e r   -   1 )   %   M A X _ T A R G E T S ;   }  
         }  
         # e n d r e g i o n   P U B L I C _ M E M B E R S  
  
  
         # r e g i o n   P R I V A T E _ M E M B E R S  
         p r i v a t e   c o n s t   i n t   M A X _ T A R G E T S   =   1 ;  
         p r i v a t e   U s e r D e f i n e d T a r g e t B u i l d i n g B e h a v i o u r   m T a r g e t B u i l d i n g B e h a v i o u r ;  
         p r i v a t e   Q u a l i t y D i a l o g   m Q u a l i t y D i a l o g ;  
         p r i v a t e   O b j e c t T r a c k e r   m O b j e c t T r a c k e r ;  
  
  
         / /   D a t a S e t   t h a t   n e w l y   d e f i n e d   t a r g e t s   a r e   a d d e d   t o  
         p r i v a t e   D a t a S e t   m B u i l t D a t a S e t ;  
          
         / /   C u r r e n t l y   o b s e r v e d   f r a m e   q u a l i t y  
         p r i v a t e   I m a g e T a r g e t B u i l d e r . F r a m e Q u a l i t y   m F r a m e Q u a l i t y   =   I m a g e T a r g e t B u i l d e r . F r a m e Q u a l i t y . F R A M E _ Q U A L I T Y _ N O N E ;  
          
         / /   C o u n t e r   u s e d   t o   n a m e   n e w l y   c r e a t e d   t a r g e t s  
         p r i v a t e   i n t   m T a r g e t C o u n t e r ;  
  
         p r i v a t e   T r a c k a b l e S e t t i n g s   m T r a c k a b l e S e t t i n g s ;  
         # e n d r e g i o n   / / P R I V A T E _ M E M B E R S  
  
  
         # r e g i o n   M O N O B E H A V I O U R _ M E T H O D S  
         p u b l i c   v o i d   S t a r t ( )  
         {  
                 t a k e P h o t o   =   G a m e O b j e c t . F i n d ( " t a k e P h o t o " ) ;  
                 v i e w G r a p h s   =   G a m e O b j e c t . F i n d ( " v i e w G r a p h s " ) ;  
                 e d i t G r a p h   =   G a m e O b j e c t . F i n d ( " e d i t G r a p h " ) ;  
                 s h a r e G r a p h   =   G a m e O b j e c t . F i n d ( " s h a r e G r a p h " ) ;  
 	 	 b a c k B t n   =   G a m e O b j e c t . F i n d ( " b a c k B t n " ) ;  
 	 	 f o r m   =   G a m e O b j e c t . F i n d ( " F o r m " ) ;  
 	 	 l o a d S c r e e n   =   G a m e O b j e c t . F i n d   ( " L o a d S c r e e n " ) ;  
  
 	 	 b a c k B t n . S e t A c t i v e   ( f a l s e ) ;  
 	 	 f o r m . S e t A c t i v e   ( f a l s e ) ;  
                 s h a r e G r a p h . S e t A c t i v e ( f a l s e ) ;  
                 e d i t G r a p h . S e t A c t i v e ( f a l s e ) ;  
 	 	 l o a d S c r e e n . S e t A c t i v e   ( f a l s e ) ;  
  
                 m T a r g e t B u i l d i n g B e h a v i o u r   =   G e t C o m p o n e n t < U s e r D e f i n e d T a r g e t B u i l d i n g B e h a v i o u r > ( ) ;  
                 i f   ( m T a r g e t B u i l d i n g B e h a v i o u r )  
                 {  
                         m T a r g e t B u i l d i n g B e h a v i o u r . R e g i s t e r E v e n t H a n d l e r ( t h i s ) ;  
                         D e b u g . L o g ( " R e g i s t e r i n g   U s e r   D e f i n e d   T a r g e t   e v e n t   h a n d l e r . " ) ;  
                 }  
  
                 m T r a c k a b l e S e t t i n g s   =   F i n d O b j e c t O f T y p e < T r a c k a b l e S e t t i n g s > ( ) ;  
                 m Q u a l i t y D i a l o g   =   F i n d O b j e c t O f T y p e < Q u a l i t y D i a l o g > ( ) ;  
                 i f   ( m Q u a l i t y D i a l o g )  
                 {  
                         m Q u a l i t y D i a l o g . g a m e O b j e c t . S e t A c t i v e ( f a l s e ) ;  
                 }  
                          
         }  
         # e n d r e g i o n   / / M O N O B E H A V I O U R _ M E T H O D S  
  
  
         # r e g i o n   I U s e r D e f i n e d T a r g e t E v e n t H a n d l e r   i m p l e m e n t a t i o n  
         / / /   < s u m m a r y >  
         / / /   C a l l e d   w h e n   U s e r D e f i n e d T a r g e t B u i l d i n g B e h a v i o u r   h a s   b e e n   i n i t i a l i z e d   s u c c e s s f u l l y  
         / / /   < / s u m m a r y >  
         p u b l i c   v o i d   O n I n i t i a l i z e d   ( )  
         {  
                 m O b j e c t T r a c k e r   =   T r a c k e r M a n a g e r . I n s t a n c e . G e t T r a c k e r < O b j e c t T r a c k e r > ( ) ;  
                 i f   ( m O b j e c t T r a c k e r   ! =   n u l l )  
                 {  
                         / /   C r e a t e   a   n e w   d a t a s e t  
                         m B u i l t D a t a S e t   =   m O b j e c t T r a c k e r . C r e a t e D a t a S e t ( ) ;  
                         m O b j e c t T r a c k e r . A c t i v a t e D a t a S e t ( m B u i l t D a t a S e t ) ;  
                 }  
         }  
  
         / / /   < s u m m a r y >  
         / / /   U p d a t e s   t h e   c u r r e n t   f r a m e   q u a l i t y  
         / / /   < / s u m m a r y >  
         p u b l i c   v o i d   O n F r a m e Q u a l i t y C h a n g e d ( I m a g e T a r g e t B u i l d e r . F r a m e Q u a l i t y   f r a m e Q u a l i t y )  
         {  
                 m F r a m e Q u a l i t y   =   f r a m e Q u a l i t y ;  
                 i f   ( m F r a m e Q u a l i t y   = =   I m a g e T a r g e t B u i l d e r . F r a m e Q u a l i t y . F R A M E _ Q U A L I T Y _ L O W )  
                 {  
                         D e b u g . L o g ( " L o w   c a m e r a   i m a g e   q u a l i t y " ) ;  
                 }  
         }  
  
 	 / / /   < s u m m a r y >  
 	 / / /   F i n d s   n o n - d i g i t   c h a r a c t e r s   i n   s t r   p a r a m e t e r   a n d   r e p l a c e s   t h e m   w i t h   n e a r e s t   e s t i m a t e   d i g i t s .  
 	 / / /   < / s u m m a r y >  
 	 	  
 	 p u b l i c   v o i d   e d i t D a t a C l i c k e d ( ) {  
 	 	 U I . G e t C o m p o n e n t < U I >   ( ) . i n i t i a l i s e F o r m   ( D a t a s e t . d a t a s e t ) ;  
 	 	 f o r m . S e t A c t i v e   ( t r u e ) ;  
 	 }  
  
         / / /   < s u m m a r y >  
         / / /   T a k e s   a   n e w   t r a c k a b l e   s o u r c e   a n d   a d d s   i t   t o   t h e   d a t a s e t  
         / / /   T h i s   g e t s   c a l l e d   a u t o m a t i c a l l y   a s   s o o n   a s   y o u   ' B u i l d N e w T a r g e t   w i t h   U s e r D e f i n e d T a r g e t B u i l d i n g B e h a v i o u r  
         / / /   < / s u m m a r y >  
 	 p u b l i c   v o i d   O n N e w T r a c k a b l e S o u r c e ( T r a c k a b l e S o u r c e   t r a c k a b l e S o u r c e )  
 	 {  
 	 	 m T a r g e t C o u n t e r + + ;  
  
 	 	 / /   D e a c t i v a t e s   t h e   d a t a s e t   f i r s t  
 	 	 m O b j e c t T r a c k e r . D e a c t i v a t e D a t a S e t ( m B u i l t D a t a S e t ) ;  
  
 	 	 / /   D e s t r o y   t h e   o l d e s t   t a r g e t   i f   t h e   d a t a s e t   i s   f u l l   o r   t h e   d a t a s e t    
 	 	 / /   a l r e a d y   c o n t a i n s   f i v e   u s e r - d e f i n e d   t a r g e t s .  
 	 	 i f   ( m B u i l t D a t a S e t . H a s R e a c h e d T r a c k a b l e L i m i t ( )   | |   m B u i l t D a t a S e t . G e t T r a c k a b l e s ( ) . C o u n t ( )   > =   M A X _ T A R G E T S )  
 	 	 {  
 	 	 	 I E n u m e r a b l e < T r a c k a b l e >   t r a c k a b l e s   =   m B u i l t D a t a S e t . G e t T r a c k a b l e s ( ) ;  
 	 	 	 f o r e a c h   ( T r a c k a b l e   t r a c k a b l e   i n   t r a c k a b l e s )  
 	 	 	 {  
 	 	 	 	 m B u i l t D a t a S e t . D e s t r o y ( t r a c k a b l e ,   t r u e ) ;  
 	 	 	 }  
 	 	 }  
  
  
 	 	 / /   G e t   p r e d e f i n e d   t r a c k a b l e   a n d   i n s t a n t i a t e   i t               - - - - -     A d d   m o r e   t a r g e t s  
 	 	 / * I m a g e T a r g e t B e h a v i o u r   i m a g e T a r g e t C o p y   =   ( I m a g e T a r g e t B e h a v i o u r ) I n s t a n t i a t e ( I m a g e T a r g e t T e m p l a t e ) ;  
 	 	 i m a g e T a r g e t C o p y . g a m e O b j e c t . n a m e   =   " U s e r D e f i n e d T a r g e t - "   +   m T a r g e t C o u n t e r ; * /  
  
 	 	 / /   A d d   t h e   d u p l i c a t e d   t r a c k a b l e   t o   t h e   d a t a   s e t   a n d   a c t i v a t e   i t  
 	 	 m B u i l t D a t a S e t . C r e a t e T r a c k a b l e ( t r a c k a b l e S o u r c e ,   I m a g e T a r g e t T e m p l a t e . g a m e O b j e c t ) ;  
  
 	 	 / /   A c t i v a t e   t h e   d a t a s e t   a g a i n  
 	 	 m O b j e c t T r a c k e r . A c t i v a t e D a t a S e t ( m B u i l t D a t a S e t ) ;  
  
 	 	 / /   E x t e n d e d   T r a c k i n g   w i t h   u s e r   d e f i n e d   t a r g e t s   o n l y   w o r k s   w i t h   t h e   m o s t   r e c e n t l y   d e f i n e d   t a r g e t .  
 	 	 / /   I f   t r a c k i n g   i s   e n a b l e d   o n   p r e v i o u s   t a r g e t ,   i t   w i l l   n o t   w o r k   o n   n e w l y   d e f i n e d   t a r g e t .  
 	 	 / /   D o n ' t   n e e d   t o   c a l l   t h i s   i f   y o u   d o n ' t   c a r e   a b o u t   e x t e n d e d   t r a c k i n g .  
 	 	 S t o p E x t e n d e d T r a c k i n g ( ) ;  
 	 	 m O b j e c t T r a c k e r . S t o p ( ) ;  
 	 	 m O b j e c t T r a c k e r . R e s e t E x t e n d e d T r a c k i n g ( ) ;  
 	 	 m O b j e c t T r a c k e r . S t a r t ( ) ;  
 	 }  
         # e n d r e g i o n   I U s e r D e f i n e d T a r g e t E v e n t H a n d l e r   i m p l e m e n t a t i o n  
  
         / / f u n c t i o n   c a l l e d   f r o m   a   b u t t o n  
 	 p u b l i c   v o i d   B u t t o n S h a r e   ( )  
 	 {  
 	 	 s h a r e G r a p h . S e t A c t i v e ( f a l s e ) ;  
 	 	 i f ( ! i s P r o c e s s i n g ) {  
 	 	 	 S t a r t C o r o u t i n e (   S h a r e S c r e e n s h o t ( )   ) ;  
 	 	 }  
 	 }  
 	 p u b l i c   I E n u m e r a t o r   S h a r e S c r e e n s h o t ( )  
 	 {  
 	 	 i s P r o c e s s i n g   =   t r u e ;  
 	 	 / /   w a i t   f o r   g r a p h i c s   t o   r e n d e r  
 	 	 y i e l d   r e t u r n   n e w   W a i t F o r E n d O f F r a m e ( ) ;  
 	 	 / / - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -   P H O T O  
 	 	 / /   c r e a t e   t h e   t e x t u r e  
 	 	 T e x t u r e 2 D   s c r e e n T e x t u r e   =   n e w   T e x t u r e 2 D ( S c r e e n . w i d t h ,   S c r e e n . h e i g h t , T e x t u r e F o r m a t . R G B 2 4 , t r u e ) ;  
 	 	 / /   p u t   b u f f e r   i n t o   t e x t u r e  
 	 	 s c r e e n T e x t u r e . R e a d P i x e l s ( n e w   R e c t ( 0 f ,   0 f ,   S c r e e n . w i d t h ,   S c r e e n . h e i g h t ) , 0 , 0 ) ;  
 	 	 / /   a p p l y  
 	 	 s c r e e n T e x t u r e . A p p l y ( ) ;  
 	 	 / / - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -   P H O T O  
 	 	 b y t e [ ]   d a t a T o S a v e   =   s c r e e n T e x t u r e . E n c o d e T o P N G ( ) ;  
 	 	 s t r i n g   d e s t i n a t i o n   =   P a t h . C o m b i n e ( A p p l i c a t i o n . p e r s i s t e n t D a t a P a t h , S y s t e m . D a t e T i m e . N o w . T o S t r i n g ( " y y y y - M M - d d - H H m m s s " )   +   " . p n g " ) ;  
 	 	 F i l e . W r i t e A l l B y t e s ( d e s t i n a t i o n ,   d a t a T o S a v e ) ;  
 	 	 i f ( ! A p p l i c a t i o n . i s E d i t o r )  
 	 	 {  
 	 	 	 / /   b l o c k   t o   o p e n   t h e   f i l e   a n d   s h a r e   i t   - - - - - - - - - - - - S T A R T  
 	 	 	 A n d r o i d J a v a C l a s s   i n t e n t C l a s s   =   n e w   A n d r o i d J a v a C l a s s ( " a n d r o i d . c o n t e n t . I n t e n t " ) ;  
 	 	 	 A n d r o i d J a v a O b j e c t   i n t e n t O b j e c t   =   n e w   A n d r o i d J a v a O b j e c t ( " a n d r o i d . c o n t e n t . I n t e n t " ) ;  
 	 	 	 i n t e n t O b j e c t . C a l l < A n d r o i d J a v a O b j e c t > ( " s e t A c t i o n " ,   i n t e n t C l a s s . G e t S t a t i c < s t r i n g > ( " A C T I O N _ S E N D " ) ) ;  
 	 	 	 A n d r o i d J a v a C l a s s   u r i C l a s s   =   n e w   A n d r o i d J a v a C l a s s ( " a n d r o i d . n e t . U r i " ) ;  
 	 	 	 A n d r o i d J a v a O b j e c t   u r i O b j e c t   =   u r i C l a s s . C a l l S t a t i c < A n d r o i d J a v a O b j e c t > ( " p a r s e " , " f i l e : / / "   +   d e s t i n a t i o n ) ;  
 	 	 	 i n t e n t O b j e c t . C a l l < A n d r o i d J a v a O b j e c t > ( " p u t E x t r a " ,   i n t e n t C l a s s . G e t S t a t i c < s t r i n g > ( " E X T R A _ S T R E A M " ) ,   u r i O b j e c t ) ;  
  
 	 	 	 i n t e n t O b j e c t . C a l l < A n d r o i d J a v a O b j e c t >   ( " s e t T y p e " ,   " t e x t / p l a i n " ) ;  
 	 	 	 i n t e n t O b j e c t . C a l l < A n d r o i d J a v a O b j e c t > ( " p u t E x t r a " ,   i n t e n t C l a s s . G e t S t a t i c < s t r i n g > ( " E X T R A _ T E X T " ) ,   " " +   m e ) ;  
 	 	 	 i n t e n t O b j e c t . C a l l < A n d r o i d J a v a O b j e c t > ( " p u t E x t r a " ,   i n t e n t C l a s s . G e t S t a t i c < s t r i n g > ( " E X T R A _ S U B J E C T " ) ,   " S U B J E C T " ) ;  
  
 	 	 	 i n t e n t O b j e c t . C a l l < A n d r o i d J a v a O b j e c t > ( " s e t T y p e " ,   " i m a g e / j p e g " ) ;  
 	 	 	 A n d r o i d J a v a C l a s s   u n i t y   =   n e w   A n d r o i d J a v a C l a s s ( " c o m . u n i t y 3 d . p l a y e r . U n i t y P l a y e r " ) ;  
 	 	 	 A n d r o i d J a v a O b j e c t   c u r r e n t A c t i v i t y   =   u n i t y . G e t S t a t i c < A n d r o i d J a v a O b j e c t > ( " c u r r e n t A c t i v i t y " ) ;  
  
 	 	 	 c u r r e n t A c t i v i t y . C a l l ( " s t a r t A c t i v i t y " ,   i n t e n t O b j e c t ) ;  
 	 	 }  
 	 	 i s P r o c e s s i n g   =   f a l s e ;  
 	 	 s h a r e G r a p h . S e t A c t i v e ( t r u e ) ;  
 	 }  
  
 	 p u b l i c   v o i d   r e s e t A p p ( ) {  
 	 	 M o d e l I n s t a n t i a t o r . r e s e t G r a p h   ( ) ;  
  
 	 	 i n t   s c e n e   =   S c e n e M a n a g e r . G e t A c t i v e S c e n e   ( ) . b u i l d I n d e x ;  
 	 	 S c e n e M a n a g e r . L o a d S c e n e   ( s c e n e ,   L o a d S c e n e M o d e . S i n g l e ) ;  
 	 }  
  
 	 I E n u m e r a t o r   U p l o a d F i l e C o ( s t r i n g   u p l o a d U R L )  
 	 {  
 	 	 / / T a k e   s c r e e n   s h o t  
 	 	 y i e l d   r e t u r n   n e w   W a i t F o r E n d O f F r a m e ( ) ;  
 	 	 T e x t u r e 2 D   s c r e e n T e x t u r e   =   n e w   T e x t u r e 2 D ( ( i n t ) ( F r a m e C o o r d i n a t e s . s i z e . x   -   ( F r a m e C o o r d i n a t e s . b o r d e r W i d t h   *   2 ) ) ,   ( i n t ) ( F r a m e C o o r d i n a t e s . s i z e . y   -   ( F r a m e C o o r d i n a t e s . b o r d e r W i d t h   *   2 ) ) ,   T e x t u r e F o r m a t . R G B 2 4 ,   t r u e ) ;  
  
 	 	 / / s c r e e n T e x t u r e . R e a d P i x e l s ( n e w   R e c t ( F r a m e C o o r d i n a t e s . l e f t ,   F r a m e C o o r d i n a t e s . b o t t o m ,   F r a m e C o o r d i n a t e s . s i z e . x ,   F r a m e C o o r d i n a t e s . s i z e . y ) , 0 , 0 ) ;  
 	 	 s c r e e n T e x t u r e . R e a d P i x e l s ( n e w   R e c t ( F r a m e C o o r d i n a t e s . l e f t   +   F r a m e C o o r d i n a t e s . b o r d e r W i d t h ,   F r a m e C o o r d i n a t e s . b o t t o m   +   F r a m e C o o r d i n a t e s . b o r d e r W i d t h ,   F r a m e C o o r d i n a t e s . s i z e . x   -   ( F r a m e C o o r d i n a t e s . b o r d e r W i d t h   *   2 ) ,   F r a m e C o o r d i n a t e s . s i z e . y   -   ( F r a m e C o o r d i n a t e s . b o r d e r W i d t h   *   2 ) ) , 0 , 0 ) ;  
 	 	 / / s c r e e n T e x t u r e . R e a d P i x e l s ( n e w   R e c t ( 0 f ,   0 f + 1 5 0 ,   S c r e e n . w i d t h ,   S c r e e n . h e i g h t - 1 5 0 ) , 0 , 0 ) ;  
  
 	 	 s c r e e n T e x t u r e . A p p l y ( ) ;  
  
 	 	 / / R o t a t e   p i c t u r e s   t a k e n   i n   l a n d s c a p e  
 	 	 i f   ( ! ( I n p u t . d e v i c e O r i e n t a t i o n   = =   D e v i c e O r i e n t a t i o n . P o r t r a i t ) )  
 	 	 {  
 	 	 	 D e b u g . L o g   ( " # # # - - - O R I E N T A T I O N   S W I T C H E D   T O   L A N D S C A P E - - - # # # " ) ;  
 	 	 	 T e x t u r e 2 D   s c r e e n T e x t u r e 2   =   n e w   T e x t u r e 2 D ( ( i n t ) ( F r a m e C o o r d i n a t e s . s i z e . y   -   ( F r a m e C o o r d i n a t e s . b o r d e r W i d t h   *   2 ) ) ,   ( i n t ) ( F r a m e C o o r d i n a t e s . s i z e . x   -   ( F r a m e C o o r d i n a t e s . b o r d e r W i d t h   *   2 ) ) ,   T e x t u r e F o r m a t . R G B 2 4 , t r u e ) ;  
 	 	 	 C o l o r 3 2 [ ]   p i x e l s   =   s c r e e n T e x t u r e . G e t P i x e l s 3 2 ( ) ;  
 	 	 	 p i x e l s   =   H e l p e r s . R o t a t e M a t r i x ( p i x e l s ,   ( i n t ) ( F r a m e C o o r d i n a t e s . s i z e . x   -   ( F r a m e C o o r d i n a t e s . b o r d e r W i d t h   *   2 ) ) ,   ( i n t ) ( F r a m e C o o r d i n a t e s . s i z e . y   -   ( F r a m e C o o r d i n a t e s . b o r d e r W i d t h   *   2 ) ) ) ;  
 	 	 	 s c r e e n T e x t u r e 2 . S e t P i x e l s 3 2 ( p i x e l s ) ;    
 	 	 	 s c r e e n T e x t u r e   =   s c r e e n T e x t u r e 2 ;  
 	 	 }  
  
 	 	 / / C o n v e r t   i m a g e   t o   b y t e s  
 	 	 b y t e [ ]   p D a t a 2   =   s c r e e n T e x t u r e . E n c o d e T o J P G ( ) ;  
 	 	 / / U n i t y E n g i n e . O b j e c t . D e s t r o y ( s c r e e n T e x t u r e ) ;  
  
 	 	 l o a d S c r e e n . S e t A c t i v e   ( t r u e ) ;  
 	 	 / / C r e a t e   p o s t   r e q u e s t  
 	 	 W W W F o r m   p o s t F o r m   =   n e w   W W W F o r m ( ) ;  
  
 	 	 s t r i n g   p o s t D a t a   =   n u l l ;  
  
 	 	 b o o l   s w   =   f a l s e ;  
  
 	 	 i f   ( ! s w )   {  
 	 	 	 p o s t D a t a   =   " - - 0 9 s a d 9 8 a s 0 9 d h i d p 0 a 9 8 s o a k s c a j b v a 1 2 \ n " +  
 	 	 	 	 " C o n t e n t - T y p e :   a p p l i c a t i o n / j s o n ;   c h a r s e t = U T F - 8 \ n \ n " +  
 	 	 	 	 " { \ " e n g i n e \ " : \ " t e s s e r a c t \ " } \ n \ n " +  
 	 	 	 	 " - - 0 9 s a d 9 8 a s 0 9 d h i d p 0 a 9 8 s o a k s c a j b v a 1 2 \ n " +  
 	 	 	 	 " C o n t e n t - T y p e :   i m a g e / j p e g \ n " +  
 	 	 	 	 " C o n t e n t - D i s p o s i t i o n :   a t t a c h m e n t ;   f i l e n a m e = \ " a t t a c h m e n t . t x t \ " .   \ n \ n " ;  
 	 	 }   e l s e   {  
 	 	 	 p o s t D a t a   =   " - - 0 9 s a d 9 8 a s 0 9 d h i d p 0 a 9 8 s o a k s c a j b v a 1 2 \ n " +  
 	 	 	 " C o n t e n t - T y p e :   a p p l i c a t i o n / j s o n ;   c h a r s e t = U T F - 8 \ n \ n " +  
 	 	 	 " { \ " e n g i n e \ " : \ " t e s s e r a c t \ " ,   \ " p r e p r o c e s s o r s \ " : [ \ " s t r o k e - w i d t h - t r a n s f o r m \ " ] } \ n \ n " +  
 	 	 	 " - - 0 9 s a d 9 8 a s 0 9 d h i d p 0 a 9 8 s o a k s c a j b v a 1 2 \ n " +  
 	 	 	 " C o n t e n t - T y p e :   i m a g e / j p e g \ n " +  
 	 	 	 " C o n t e n t - D i s p o s i t i o n :   a t t a c h m e n t ;   f i l e n a m e = \ " a t t a c h m e n t . t x t \ " .   \ n \ n " ;  
 	 	 }  
 	 	 	  
  
 	 	 b y t e [ ]   p D a t a 1   =   S y s t e m . T e x t . E n c o d i n g . U T F 8 . G e t B y t e s ( p o s t D a t a . T o C h a r A r r a y ( ) ) ;  
  
 	 	 s t r i n g   p o s t D a t a E n d   =   " \ n - - 0 9 s a d 9 8 a s 0 9 d h i d p 0 a 9 8 s o a k s c a j b v a 1 2 - - \ n " ;  
  
 	 	 b y t e [ ]   p D a t a 3   =   S y s t e m . T e x t . E n c o d i n g . U T F 8 . G e t B y t e s ( p o s t D a t a E n d . T o C h a r A r r a y ( ) ) ;  
  
 	 	 / / c o m b i n e   d i f f e r e n t   p a r t s   o f   r e q u e s t   i n t o   b y t e   a r r a y  
 	 	 b y t e [ ]   p D a t a C o m   =   n e w   b y t e [ p D a t a 1 . L e n g t h   +   p D a t a 2 . L e n g t h   +   p D a t a 3 . L e n g t h ] ;  
 	 	 S y s t e m . B u f f e r . B l o c k C o p y ( p D a t a 1 ,   0 ,   p D a t a C o m ,   0 ,   p D a t a 1 . L e n g t h ) ;  
 	 	 S y s t e m . B u f f e r . B l o c k C o p y ( p D a t a 2 ,   0 ,   p D a t a C o m ,   p D a t a 1 . L e n g t h ,   p D a t a 2 . L e n g t h ) ;  
 	 	 S y s t e m . B u f f e r . B l o c k C o p y ( p D a t a 3 ,   0 ,   p D a t a C o m ,   p D a t a 1 . L e n g t h   +   p D a t a 2 . L e n g t h ,   p D a t a 3 . L e n g t h ) ;  
  
 	 	 S y s t e m . C o l l e c t i o n s . G e n e r i c . D i c t i o n a r y < s t r i n g ,   s t r i n g >   h e a d e r s   =   n e w   S y s t e m . C o l l e c t i o n s . G e n e r i c . D i c t i o n a r y < s t r i n g ,   s t r i n g > ( ) ;  
 	 	 h e a d e r s . A d d ( " C o n t e n t - T y p e " ,   " m u l t i p a r t / r e l a t e d ; b o u n d a r y = 0 9 s a d 9 8 a s 0 9 d h i d p 0 a 9 8 s o a k s c a j b v a 1 2 " ) ;  
  
 	 	 b y t e [ ]   p D a t a   =   S y s t e m . T e x t . E n c o d i n g . U T F 8 . G e t B y t e s ( p o s t D a t a . T o C h a r A r r a y ( ) ) ;  
  
 	 	 / / C r e a t e   c o n n e c t i o n  
 	 	 W W W   u p l o a d   =   n e w   W W W ( u p l o a d U R L ,   p D a t a C o m , h e a d e r s ) ;  
 	 	 y i e l d   r e t u r n   u p l o a d ;  
 	 	 i f   ( u p l o a d . e r r o r   = =   n u l l )  
 	 	 {  
 	 	 	 D e b u g . L o g ( " \ n \ n " + S y s t e m . D a t e T i m e . N o w . T o S t r i n g ( ) + " :   O C R   R E S U L T :   " + u p l o a d . t e x t ) ;  
 	 	 	 s t r i n g   r e s u l t   =   u p l o a d . t e x t . T r i m ( ) ;  
 	 	 	 r e s u l t   + =   " \ n " ;  
  
 	 	 	 M a k e D a t a s e t   ( r e s u l t ) ;  
 	 	 	 b a c k B t n . S e t A c t i v e ( t r u e ) ;  
 	 	 	 s h a r e G r a p h . S e t A c t i v e ( t r u e ) ;  
 	 	 	 e d i t G r a p h . S e t A c t i v e ( t r u e ) ;  
 	 	 	 v i e w G r a p h s . S e t A c t i v e ( f a l s e ) ;  
 	 	 	 t a k e P h o t o . S e t A c t i v e ( f a l s e ) ;  
 	 	 	 f r a m e C a n v a s . S e t A c t i v e   ( f a l s e ) ;  
 	 	 }  
 	 	 e l s e  
 	 	 {  
 	 	 	 D e b u g . L o g ( " W W W   E r r o r :   " +   u p l o a d . e r r o r ) ;  
 	 	 	 D e b u g . L o g ( " W W W   E r r o r :   " +   u p l o a d . t e x t ) ;  
 	 	 	 l o a d S c r e e n . S e t A c t i v e   ( f a l s e ) ;  
 	 	 }  
  
  
 	 }  
 	 v o i d   U p l o a d F i l e ( s t r i n g   u p l o a d U R L )  
 	 { 	  
 	 	   S t a r t C o r o u t i n e ( U p l o a d F i l e C o ( u p l o a d U R L ) ) ;  
 	 }  
  
 	 / / G e n e r a t e s   a   D a t a s e t   f r o m   t h e   O C R   r e s u l t  
 	 v o i d   M a k e D a t a s e t ( s t r i n g   t e x t ) {  
 	 	 D a t a s e t . T y p e s   t y p e   =   D a t a s e t . T y p e s . N u l l ;  
 	 	 s t r i n g   f i r s t l i n e   =   t e x t . S u b s t r i n g ( 0 ,   t e x t . I n d e x O f ( " \ n " ) ) ;  
 	 	 t r y { 	  
 	 	 	 t e x t   =   t e x t . R e p l a c e ( f i r s t l i n e ,   n u l l ) ;  
 	 	 }   c a t c h   {  
 	 	 	 r e s e t A p p   ( ) ;  
 	 	 }  
 	 	 D e b u g . L o g   ( " T i t l e :   "   +   f i r s t l i n e + " \ n " ) ;  
 	 	 t e x t   =   t e x t . T r i m S t a r t ( ) ;  
  
 	 	 s t r i n g   s e r   =   t e x t . S u b s t r i n g ( 0 ,   t e x t . I n d e x O f ( " \ n " ) ) ;  
 	 	 t r y {  
 	 	 	 t e x t   =   t e x t . R e p l a c e ( s e r ,   " " ) ;  
 	 	 }   c a t c h   {  
 	 	 	 r e s e t A p p   ( ) ;  
 	 	 }  
 	 	 s t r i n g [ ]   t e m p S e r   =   s e r . S p l i t   ( '   ' ) ;  
  
 	 	 s t r i n g [ ]   s e r i e s   =   n e w   s t r i n g [ t e m p S e r . L e n g t h   -   1 ] ;  
  
 	 	 f o r ( i n t   i   =   1 ;   i   <   t e m p S e r . L e n g t h ;   i + + ) {  
 	 	 	 s e r i e s   [ i   -   1 ]   =   t e m p S e r   [ i ] ;  
 	 	 }  
  
 	 	 D e b u g . L o g   ( " S e r i e s :   "   +   s e r + " \ n " ) ;  
 	 	 t e x t   =   t e x t . T r i m S t a r t ( ) ;  
  
  
  
 	 	 S y s t e m . C o l l e c t i o n s . G e n e r i c . L i s t < s t r i n g [ ] >   t a b l e L i s t   =   n e w   S y s t e m . C o l l e c t i o n s . G e n e r i c . L i s t < s t r i n g [ ] > ( ) ;  
 	 	 S y s t e m . C o l l e c t i o n s . G e n e r i c . L i s t < s t r i n g >   c a t e g o r y L i s t   =   n e w   S y s t e m . C o l l e c t i o n s . G e n e r i c . L i s t < s t r i n g > ( ) ;  
 	 	 s t r i n g   t e m p   =   " " ;  
 	 	 b o o l   s u c c e s s   =   t r u e ;  
  
 	 	 c o n s t   i n t   M I N _ L E N G T H   =   1 ;  
 	 	 w h i l e ( t e x t . L e n g t h   >   M I N _ L E N G T H   & &   t e x t . I n d e x O f ( " \ n " )   >   0 ) {  
 	 	 	 t e m p   =   t e x t . S u b s t r i n g ( 0 ,   t e x t . I n d e x O f ( " \ n " ) ) ;  
 	 	 	 t e x t   =   t e x t . R e p l a c e ( t e m p ,   " " ) ;  
  
 	 	 	 / / R e m o v e s   s e r i e s   e n t r i e s   f r o m   r o w s  
 	 	 	 s t r i n g [ ]   r o w A n d C a t e g o r y   =   t e m p . S p l i t ( '   ' ) ;  
 	 	 	 s t r i n g [ ]   r o w   =   n e w   s t r i n g [ r o w A n d C a t e g o r y . L e n g t h - 1 ] ;  
 	 	 	 c a t e g o r y L i s t . A d d   ( r o w A n d C a t e g o r y   [ 0 ] ) ;  
 	 	 	 f o r ( i n t   i   =   1 ;   i   <   r o w A n d C a t e g o r y . L e n g t h ;   i + + ) {  
 	 	 	 	 i f   ( ! r o w A n d C a t e g o r y   [ i ] . A l l   ( c h a r . I s D i g i t ) )   {  
 	 	 	 	 	 r o w   [ i   -   1 ]   =   H e l p e r s . s a n i t i z e S t r i n g (   r o w A n d C a t e g o r y   [ i ]   ) ;  
 	 	 	 	 }   e l s e   {  
 	 	 	 	 	 r o w   [ i   -   1 ]   =   r o w A n d C a t e g o r y   [ i ] ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 t a b l e L i s t . A d d ( r o w ) ;  
 	 	 	 D e b u g . L o g   ( " R o w :   "   +   s t r i n g . J o i n ( " " , t a b l e L i s t . E l e m e n t A t ( t a b l e L i s t . C o u n t - 1 ) ) + " \ n " ) ;  
 	 	 	 t e x t   =   t e x t . T r i m S t a r t ( ) ;  
 	 	 }  
 	 	 s t r i n g [ ]   c a t e g o r i e s   =   c a t e g o r y L i s t . T o A r r a y   ( ) ;  
  
 	 	 f l o a t [ , ]   t a b l e   =   n e w   f l o a t [ s e r i e s . L e n g t h ,   t a b l e L i s t . C o u n t ] ;  
 	 	 f o r ( i n t   i   =   0 ;   i   <   t a b l e L i s t . C o u n t ;   i + + ) {  
 	 	 	 f o r ( i n t   x   =   0 ;   x   <   s e r i e s . L e n g t h ;   x + + ) {  
 	 	 	 	 f l o a t   t e m p F l o a t   =   0 ;  
  
 	 	 	 	 t r y { 	  
 	 	 	 	 	 i f   ( S i n g l e . T r y P a r s e   ( t a b l e L i s t . E l e m e n t A t   ( i )   [ x ] ,   o u t   t e m p F l o a t ) )   {  
 	 	 	 	 	 }   e l s e   i f   ( S i n g l e . T r y P a r s e   ( H e l p e r s . s a n i t i z e S t r i n g   ( t a b l e L i s t . E l e m e n t A t   ( i )   [ x ] ) ,   o u t   t e m p F l o a t ) )   {  
 	 	 	 	 	 }   e l s e   {  
 	 	 	 	 	 	 t e m p F l o a t   =   0 ;  
 	 	 	 	 	 	 s u c c e s s   =   f a l s e ;  
 	 	 	 	 	 }  
 	 	 	 	 } c a t c h   ( E x c e p t i o n   e ) {  
 	 	 	 	 	 t e m p F l o a t   =   0 ;  
 	 	 	 	 	 s u c c e s s   =   f a l s e ;  
 	 	 	 	 }  
  
 	 	 	 	 t a b l e   [ x ,   i ]   =   t e m p F l o a t ;  
 	 	 	 }  
 	 	 }  
  
  
  
 	 	 s t r i n g   d b g   =   " " ;  
 	 	 f o r ( i n t   i   =   0 ;   i   <   t a b l e L i s t . C o u n t ;   i + + ) {  
 	 	 	 d b g   =   " " ;  
 	 	 	 d b g   + =   c a t e g o r i e s   [ i ]   +   "   " ;  
 	 	 	 f o r ( i n t   x   =   0 ;   x   <   s e r i e s . L e n g t h ;   x + + ) {  
 	 	 	 	 d b g   + =   "   :   "   +   t a b l e [ x , i ] ;  
 	 	 	 }  
 	 	 	 D e b u g . L o g   ( d b g ) ;  
 	 	 }  
  
 	 	 / / S e n d   t o   f o r m  
 	 	 D a t a s e t   d a t a s e t   =   n e w   D a t a s e t   ( t y p e ,   f i r s t l i n e ,   c a t e g o r i e s ,   s e r i e s ,   " " ,   " " ,   t a b l e ,   c a t e g o r i e s . L e n g t h ,   s e r i e s . L e n g t h ) ;  
 	 	 U I . G e t C o m p o n e n t < U I >   ( ) . i n i t i a l i s e F o r m   ( d a t a s e t ) ;  
 	 	 l o a d S c r e e n . S e t A c t i v e   ( f a l s e ) ;  
 	 	 f o r m . S e t A c t i v e   ( t r u e ) ;  
  
 	 	 / / C r e a t e   g r a p h   d i r e c t l y  
 	 	 / / D a t a s e t . d a t a s e t   =   n e w   D a t a s e t   ( t y p e ,   f i r s t l i n e ,   c a t e g o r i e s ,   s e r i e s ,   t e m p S e r [ 0 ] ,   f i r s t l i n e ,   t a b l e ,   c a t e g o r i e s . L e n g t h ,   s e r i e s . L e n g t h ) ;  
 	 }  
  
  
  
         # r e g i o n   P U B L I C _ M E T H O D S  
         / / /   < s u m m a r y >  
         / / /   I n s t a n t i a t e s   a   n e w   u s e r - d e f i n e d   t a r g e t   a n d   i s   a l s o   r e s p o n s i b l e   f o r   d i s p a t c h i n g   c a l l b a c k   t o    
         / / /   I U s e r D e f i n e d T a r g e t E v e n t H a n d l e r : : O n N e w T r a c k a b l e S o u r c e  
         / / /   < / s u m m a r y >  
  
         p u b l i c   v o i d   B u i l d N e w T a r g e t ( )  
         {  
                 i f   ( m F r a m e Q u a l i t y   = =   I m a g e T a r g e t B u i l d e r . F r a m e Q u a l i t y . F R A M E _ Q U A L I T Y _ M E D I U M   | |    
                         m F r a m e Q u a l i t y   = =   I m a g e T a r g e t B u i l d e r . F r a m e Q u a l i t y . F R A M E _ Q U A L I T Y _ H I G H )  
                 {  
                         / /   c r e a t e   t h e   n a m e   o f   t h e   n e x t   t a r g e t .  
                         / /   t h e   T r a c k a b l e N a m e   o f   t h e   o r i g i n a l ,   l i n k e d   I m a g e T a r g e t B e h a v i o u r   i s   e x t e n d e d   w i t h   a   c o n t i n u o u s   n u m b e r   t o   e n s u r e   u n i q u e   n a m e s  
                         s t r i n g   t a r g e t N a m e   =   s t r i n g . F o r m a t ( " { 0 } - { 1 } " ,   I m a g e T a r g e t T e m p l a t e . T r a c k a b l e N a m e ,   m T a r g e t C o u n t e r ) ;  
  
                         / /   g e n e r a t e   a   n e w   t a r g e t :  
                         m T a r g e t B u i l d i n g B e h a v i o u r . B u i l d N e w T a r g e t ( t a r g e t N a m e ,   I m a g e T a r g e t T e m p l a t e . G e t S i z e ( ) . x ) ;  
 	 	 	 U p l o a d F i l e ( " h t t p : / / 1 0 5 . 2 5 5 . 1 6 8 . 1 1 5 : 9 2 9 2 / o c r - f i l e - u p l o a d " ) ;  
  
                 }  
                 e l s e  
                 {  
                         D e b u g . L o g ( " C a n n o t   b u i l d   n e w   t a r g e t ,   d u e   t o   p o o r   c a m e r a   i m a g e   q u a l i t y " ) ;  
                         i f   ( m Q u a l i t y D i a l o g )  
                         {  
                                 m Q u a l i t y D i a l o g . g a m e O b j e c t . S e t A c t i v e ( t r u e ) ;  
                         }  
                 }  
         }  
  
         p u b l i c   v o i d   C l o s e Q u a l i t y D i a l o g ( )  
         {  
                 i f   ( m Q u a l i t y D i a l o g )  
                         m Q u a l i t y D i a l o g . g a m e O b j e c t . S e t A c t i v e ( f a l s e ) ;  
         }  
         # e n d r e g i o n   / / P U B L I C _ M E T H O D S  
  
  
         # r e g i o n   P R I V A T E _ M E T H O D S  
         / / /   < s u m m a r y >  
         / / /   T h i s   m e t h o d   o n l y   d e m o n s t r a t e s   h o w   t o   h a n d l e   e x t e n d e d   t r a c k i n g   f e a t u r e   w h e n   y o u   h a v e   m u l t i p l e   t a r g e t s   i n   t h e   s c e n e  
         / / /   S o ,   t h i s   m e t h o d   c o u l d   b e   r e m o v e d   o t h e r w i s e  
         / / /   < / s u m m a r y >  
         / / /    
         p r i v a t e   v o i d   S t o p E x t e n d e d T r a c k i n g ( )  
         {  
                 / /   I f   E x t e n d e d   T r a c k i n g   i s   e n a b l e d ,   w e   f i r s t   d i s a b l e   i t   f o r   a l l   t h e   t r a c k a b l e s  
                 / /   a n d   t h e n   e n a b l e   i t   o n l y   f o r   t h e   n e w l y   c r e a t e d   t a r g e t  
                 b o o l   e x t T r a c k i n g E n a b l e d   =   m T r a c k a b l e S e t t i n g s   & &   m T r a c k a b l e S e t t i n g s . I s E x t e n d e d T r a c k i n g E n a b l e d ( ) ;  
                 i f   ( e x t T r a c k i n g E n a b l e d )  
                 {  
                         S t a t e M a n a g e r   s t a t e M a n a g e r   =   T r a c k e r M a n a g e r . I n s t a n c e . G e t S t a t e M a n a g e r ( ) ;  
  
                         / /   1 .   S t o p   e x t e n d e d   t r a c k i n g   o n   a l l   t h e   t r a c k a b l e s  
                         f o r e a c h ( v a r   t b   i n   s t a t e M a n a g e r . G e t T r a c k a b l e B e h a v i o u r s ( ) )  
                         {  
                                 v a r   i t b   =   t b   a s   I m a g e T a r g e t B e h a v i o u r ;  
                                 i f   ( i t b   ! =   n u l l )  
                                 {  
                                         i t b . I m a g e T a r g e t . S t o p E x t e n d e d T r a c k i n g ( ) ;  
                                 }  
                         }  
          
                         / /   2 .   S t a r t   E x t e n d e d   T r a c k i n g   o n   t h e   m o s t   r e c e n t l y   a d d e d   t a r g e t  
                         L i s t < T r a c k a b l e B e h a v i o u r >   t r a c k a b l e L i s t   =     s t a t e M a n a g e r . G e t T r a c k a b l e B e h a v i o u r s ( ) . T o L i s t ( ) ;  
                         I m a g e T a r g e t B e h a v i o u r   l a s t I t b   =   t r a c k a b l e L i s t [ L a s t T a r g e t I n d e x ]   a s   I m a g e T a r g e t B e h a v i o u r ;  
                         i f   ( l a s t I t b   ! =   n u l l )  
                         {  
                                 i f   ( l a s t I t b . I m a g e T a r g e t . S t a r t E x t e n d e d T r a c k i n g ( ) )  
                                         D e b u g . L o g ( " E x t e n d e d   T r a c k i n g   s u c c e s s f u l l y   e n a b l e d   f o r   "   +   l a s t I t b . n a m e ) ;  
                         }  
                 }  
         }  
  
  
         # e n d r e g i o n   / / P R I V A T E _ M E T H O D S  
 }  
  
  
  
 