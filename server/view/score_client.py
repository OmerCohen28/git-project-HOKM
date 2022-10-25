from tkinter import *

# DATA = [("isdfsdfsdfdan", 2), ("ran", 1), ("omer", 2), ("ron", 1)]
WIDTH = 1000
HEIGHT = 650

class ScoreClient:
    def __init__(self, database):
        self.database = database

        self.root = Tk()

        self.root.geometry(f'{WIDTH}x{HEIGHT}+{int(self.root.winfo_screenwidth() / 2) - int(WIDTH / 2)}+{int(self.root.winfo_screenheight() / 2) - int(HEIGHT / 2)}')
        self.root.minsize(600, 300)
        self.root.configure(bg='white')

        self.next_game = None

        self.__setup_screen()

    def start(self):
        self.root.mainloop()

        return self.next_game

    def __setup_screen(self):
        score_data = self.__get_data()

        score_data.sort(key=lambda name_score_pair: -name_score_pair[1])

        title_lbl = Label(self.root, text="Hokm results", font=('Narkisim', 60, 'bold'), bg='white')
        title_lbl.pack(pady=40)

        frame = Frame(self.root, bd=1)
        frame.pack()

        name_lbl = Label(frame, text='name', font=('Arial', 26, 'bold'), bg='white', fg="#363636", padx=35, pady=15)
        name_lbl.grid(column=0, row=0, padx=2, pady=2, sticky=NSEW)

        name_lbl = Label(frame, text='score', font=('Arial', 26, 'bold'), bg='white', fg="#363636", padx=35, pady=15)
        name_lbl.grid(column=1, row=0, padx=2, pady=2, sticky=NSEW)

        for i, (name, score) in enumerate(score_data):
            name_lbl = Label(frame, text=name, font=('Arial', 24), bg='white',
                            fg="#363636", padx=35, pady=15)
            name_lbl.grid(column=0, row=i + 1, padx=2, pady=2, sticky=NSEW)

            score_lbl = Label(frame, text=f"{score}", font=('Arial', 24), bg='white',
                            fg="#363636", padx=35, pady=15)
            score_lbl.grid(column=1, row=i + 1, padx=2, pady=2, sticky=NSEW)
        
        gui_btn = Button(self.root, text='next game with gui', bd=0, font=('Narkisim', 20, 'bold'),
                        bg="#348ceb", activebackground="#8cbaff",
                        command=self.set_with_gui)
        gui_btn.place(rely=0.98, relx=0.98, anchor=S + E)

        gui_btn = Button(self.root, text='next game no gui', bd=0, font=('Narkisim', 20, 'bold'),
                        bg="#348ceb", activebackground="#8cbaff",
                        command=self.set_no_gui)
        gui_btn.place(rely=0.98, relx=0.02, anchor=S + W)
        
    def set_with_gui(self):
        self.next_game = True
        self.root.quit()

    def set_no_gui(self):
        self.next_game = False
        self.root.quit()

    def stop(self):
        self.root.destroy()

    def __get_data(self):
        # data = DATA
        data = self.database.get_scores()
        return data
